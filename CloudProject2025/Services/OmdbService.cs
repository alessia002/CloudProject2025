using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CloudProject2025.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            // Controlla se la risposta contiene un errore
            var jsonObj = JsonConvert.DeserializeObject<JObject>(contents);
            if (jsonObj?["Response"]?.ToString() == "False")
            {
                return null; // Nessun film trovato
            }

            // Se la risposta è valida, deserializza in un oggetto Film
            return JsonConvert.DeserializeObject<Film>(contents);


        }
    }


}
