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

        public async Task SendMessageAsync(string url, string message)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var jsonMessage = JsonConvert.SerializeObject(new { message = message });
            var content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to send message. Status Code: {response.StatusCode}");
            }
        }
    }
}
