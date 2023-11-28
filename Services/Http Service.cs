using Newtonsoft.Json;
using System.Text;

namespace Watcher.Services
{
    public class HttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void SendMessage(string url, string message)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var jsonMessage = JsonConvert.SerializeObject(new { message = message });

            var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

            httpClient.PostAsync(url, content);
        }
    }
}
