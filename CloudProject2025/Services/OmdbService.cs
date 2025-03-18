using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CloudProject2025.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

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

        public async Task<Models.Film?> GetFilmAsync(string title, int year)
        {
            string url = $"{BASE_URL}?apikey={_apiKey}&t={Uri.EscapeDataString(title)}";
            if (year > 0)
            {
                url += $"&y={year}";
            }
            var response = await _httpClient.GetAsync(url);
            var contents = await response.Content.ReadAsStringAsync();
            
            Film oggettoFilm = JsonConvert.DeserializeObject<Film>(contents);

            return oggettoFilm;
        }
    }


}
