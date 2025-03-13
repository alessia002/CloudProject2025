using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudProject2025.Services
{
    

    public class OmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string BASE_URL = "http://www.omdbapi.com/";

        public OmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OMDb:ApiKey"];
        }

        public async Task<Models.Film?> GetFilmAsync(string title)
        {
            string url = $"{BASE_URL}?apikey={_apiKey}&t={Uri.EscapeDataString(title)}";
            var response = await _httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<Models.Film>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }


}
