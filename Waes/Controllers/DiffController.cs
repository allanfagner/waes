using System;
using System.Data.SqlClient;
using System.Web.Http;
using System.Linq;
using Waes.Model;
using Waes.Context;
using System.Net.Http;

namespace Waes.Controllers
{
    

    [RoutePrefix("v1/diff")]
    public class DiffController : ApiController
    {
        WaesContext context;

        public DiffController(WaesContext context)
        {
            this.context = context;
        }

        public DiffController()
        {
            context = new WaesContext();           
        }

        [HttpPost]
        [Route("{id}/left")]
        public object Left(int id, Base64Request data)
        {            
            var duo = context.Base64Duo.FirstOrDefault(d => d.Id == id);
            if (duo == null)
            {
                duo = new Base64Duo(id, new Base64(data.Data), null);
                context.Base64Duo.Add(duo);
            }
            else
            {
                duo.ChangeLeftValue(new Base64(data.Data));
            }
                        
            context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("{id}/right")]
        public object Right(int id, Base64Request data)
        {
            var duo = context.Base64Duo.FirstOrDefault(d => d.Id == id);
            if (duo == null)
            {
                duo = new Base64Duo(id, null, new Base64(data.Data));
                context.Base64Duo.Add(duo);
            }
            else
            {
                duo.ChangeRightValue(new Base64(data.Data));
            }

            context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public object Diff(int id)
        {
            var duo = context.Base64Duo.FirstOrDefault(d => d.Id == id);

            if (duo == null) throw new Exception("Entry not found");
            if (duo.UnderlyingStringsAreEqual()) return "Left and rigth json are equal";
            if (!duo.UnderlyingStringsHasSameLenth()) return "Left and right json don't have the same length";

            return duo.Diff();
        }        
    }
}
