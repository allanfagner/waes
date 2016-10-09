using System.Data.Entity;
using Waes.Model;

namespace Waes.Context
{
    //Giving this is a sample, I am adding the connection string directly in the class constructor
    //In a production app that could be a value in the configuration file
    public class WaseContext : DbContext
    {
        public DbSet<Base64Duo> Base64Duo { get; set; }

        public WaseContext()
            : base(@"Data Source=C:\Users\AllanFagner\Documents\Visual Studio 2015\Projects\Waes\Waes\db\Waes.sdf;Password=test")
        {
            
        }
    }
}