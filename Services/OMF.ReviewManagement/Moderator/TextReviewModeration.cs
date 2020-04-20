using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OMF.ReviewManagement.Moderator
{
    public class TextReviewModeration : IReviewModeration
    {
        private readonly IConfiguration _configuration;
        public TextReviewModeration(IConfiguration configuration) => _configuration = configuration;

        public async Task<string> TextModerationReviewAsync(string reviewText)
        {
            using (var client = new HttpClient())
            {
                var queryString = HttpUtility.ParseQueryString(string.Empty);

                // suvankar: 
                // All the url endpoint and subscriptionkey are valid
                // I have already created one cognative service into my azure portal
                var endPoint = _configuration["TextContentModeratorApiInfo:endPoint"];
                // Request headers
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key",
                    _configuration["TextContentModeratorApiInfo:SubscriptionKey"]);

                // Request parameters
                queryString["autocorrect"] = "true";
                queryString["PII"] = "true";
                queryString["classify"] = "True";
                queryString["language"] = "eng";
                var uri = endPoint + "?" + queryString;

                HttpResponseMessage response;
                // Request body
                byte[] byteData = Encoding.UTF8.GetBytes(reviewText);

                using (var content = new ByteArrayContent(byteData))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                    response = await client.PostAsync(uri, content);
                }

                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
