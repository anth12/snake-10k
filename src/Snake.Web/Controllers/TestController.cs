using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Snake.Web.Controllers
{
    public class TestController : Controller
    {
        private static readonly int totalAllowedBytes = 10*1024;
        private const string url = "http://localhost:63761/";

        public async Task<ContentResult> Index()
        {
            var result = "";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                // Headers are not included (nor is the favicon)
                var responseString = await response.Content.ReadAsStringAsync();
                var bytes = Encoding.Unicode.GetByteCount(responseString);

                result += $"Document: {bytes}b - {bytes / 1024}kb - {(double)bytes / totalAllowedBytes * 100}% \n";

                var resources = Regex.Matches(responseString, @"(?<=src=|href=)(.*?)(?=\>)");

                foreach (Match resource in resources)
                {
                    var resourceResponse = await client.GetAsync(url + resource.Value);
                    var resourceContent = await resourceResponse.Content.ReadAsStringAsync();

                    var resourceBytes = Encoding.Unicode.GetByteCount(resourceContent);

                    result += $"- {resource.Value}: {resourceBytes}b - {resourceBytes / 1024}kb - {(double)resourceBytes / totalAllowedBytes * 100}% \n";

                    bytes += resourceBytes;
                }


                result += $"Total: {bytes}b - {bytes / 1024}kb - {(double)bytes / totalAllowedBytes * 100}%";

                return Content(result);
            }
            
        }

    }
}
