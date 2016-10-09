using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;
using Waes.Model;
namespace Waes.Tests.Functional
{
    //I prefer not use Mocks and Stubs for functional tests
    //In a automated test environment I would prefer to create the database during test warmup
    [TestClass]
    public class FunctionalTests
    {
        static string host = "http://localhost:52122";
        static string baseUrl = host + "/v1/diff/";

        [TestMethod]
        public void CreateLeftBase64_AndDiff_ReturnsStringsDoNotHAveSameLength()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage created = client.PostAsJsonAsync(baseUrl + "1/left", new { data = "VGV4dCB0byBiZSBjb252ZXJ0ZWQ=" }).Result;
            HttpResponseMessage response = client.GetAsync(baseUrl + "1").Result;
            var result = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.AreEqual("\"Left and right json don't have the same length\"", result);
        }

        [TestMethod]
        public void CreateRightBase64_AndDiff_ReturnsStringsDoNotHAveSameLength()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage created = client.PostAsJsonAsync(baseUrl + "2/right", new { data = "VGV4dCB0byBiZSBjb252ZXJ0ZWQ=" }).Result;
            HttpResponseMessage response = client.GetAsync(baseUrl + "2").Result;
            var result = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.AreEqual("\"Left and right json don't have the same length\"", result);
        }

        [TestMethod]
        public void CreateRightAndLeftBase64WithEqualString_RturnsLeftAndRightJsonAreEquals()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage leftCreated = client.PostAsJsonAsync(baseUrl + "3/left", new { data = "VGV4dCB0byBiZSBjb252ZXJ0ZWQ=" }).Result;
            HttpResponseMessage rightCreated = client.PostAsJsonAsync(baseUrl + "3/right", new { data = "VGV4dCB0byBiZSBjb252ZXJ0ZWQ=" }).Result;
            HttpResponseMessage response = client.GetAsync(baseUrl + "3").Result;
            var result = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(200, (int)response.StatusCode);
            Assert.AreEqual("\"Left and rigth json are equal\"", result);            
        }

        [TestMethod]
        public void CreateRightAndLeftBase64WithDifferentStringWithSameSize_RturnsDiffs()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage leftCreated = client.PostAsJsonAsync(baseUrl + "4/left", new { data = "dGVzdCBzdHJpbmc=" }).Result;
            HttpResponseMessage rightCreated = client.PostAsJsonAsync(baseUrl + "4/right", new { data = "VGVTdCBTdFJpTmc=" }).Result;
            HttpResponseMessage response = client.GetAsync(baseUrl + "4").Result;
            var result = response.Content.ReadAsStringAsync().Result;

            Assert.AreEqual(200, (int)response.StatusCode);
        }
    }
}
