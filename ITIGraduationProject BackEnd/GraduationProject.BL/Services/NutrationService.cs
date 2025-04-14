using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class NutritionService
    {
        private readonly HttpClient _httpClient;

        public NutritionService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://ai-nutritional-facts.p.rapidapi.com/");
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "ai-nutritional-facts.p.rapidapi.com");
            _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", config["RapidAPI:Key"]);
        }

        public async Task<NutritionInfo?> GetNutritionAsync(string input)
        {
            var body = new { input };
            var jsonContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("getNutritionalInfo", jsonContent);
            var json = await response.Content.ReadAsStringAsync();

            Console.WriteLine("📦 Raw Nutrition JSON: \n" + json);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"❌ Nutrition API failed: {response.StatusCode}");
                return null;
            }

            try
            {
                var parsed = JsonSerializer.Deserialize<NutritionInfo>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                Console.WriteLine($"✅ Parsed Calories: {parsed?.Calories}");
                return parsed;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Deserialization failed: " + ex.Message);
                return null;
            }
        }

    }


}
