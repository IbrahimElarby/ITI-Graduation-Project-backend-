using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ITIGraduationProject.BL
{
    public class ExternalRecipeService
{
    private readonly HttpClient _httpClient;

    public ExternalRecipeService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://ai-food-recipe-generator-api-custom-diet-quick-meals.p.rapidapi.com/");
        _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", "ai-food-recipe-generator-api-custom-diet-quick-meals.p.rapidapi.com");
        _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", config["RapidAPI:Key"]); // use appsettings!
    }

        public async Task<AiRecipe?> GenerateRecipeAsync(AiRecipeRequest request)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                ingredients = request.Ingredients,
                dietary_restrictions = request.DietaryRestrictions,
                cuisine = request.Cuisine,
                meal_type = request.MealType,
                servings = request.Servings,
                lang = request.Lang
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("generate?noqueue=1", content);
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AiRecipeResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Result;
        }


    }


}
