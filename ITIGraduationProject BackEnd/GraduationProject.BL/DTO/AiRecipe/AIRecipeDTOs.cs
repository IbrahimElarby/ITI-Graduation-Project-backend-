using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class AiRecipeRequest
    {
        public List<string> Ingredients { get; set; } = new();
        public List<string> DietaryRestrictions { get; set; } = new();
        public string Cuisine { get; set; } = "Any";
        public string MealType { get; set; } = "dinner";
        public int Servings { get; set; } = 2;
        public string Lang { get; set; } = "en";
    }
    public class AiRecipeResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public AiRecipe Result { get; set; }
    }

    public class AiRecipe
    {
        public string Title { get; set; }
        public List<AiIngredient> Ingredients { get; set; }
        public List<string> Instructions { get; set; }

        [JsonPropertyName("nutrition_info")]
        public AiNutrition Nutrition { get; set; }
    }

    public class AiNutrition
    {
        public int Calories { get; set; }
        public string Protein { get; set; }
        public string Fat { get; set; }
        public string Carbohydrates { get; set; }
    }
    public class AiIngredient
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("quantity")]
        public string Quantity { get; set; }
    }


}
