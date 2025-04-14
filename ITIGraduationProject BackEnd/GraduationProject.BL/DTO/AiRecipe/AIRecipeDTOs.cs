using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    public class NutritionResponse
    {
        public string FoodName { get; set; }
        public NutritionInfo Nutrition { get; set; }
    }

    public class DecimalStringFlexibleConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                if (reader.TokenType == JsonTokenType.Number)
                    return reader.GetDecimal();

                if (reader.TokenType == JsonTokenType.String)
                {
                    var raw = reader.GetString();
                    raw = raw?.ToLower().Replace("kcal", "").Replace("g", "").Replace("mg", "").Trim();

                    if (decimal.TryParse(raw, out var value))
                        return value;
                }
            }
            catch
            {
                // Optional: log warning
            }

            return 0;
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
            => writer.WriteNumberValue(value);
    }

    public class NutritionInfo
    {
        [JsonPropertyName("servings")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal Servings { get; set; }

        [JsonPropertyName("calories in kcal")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal Calories { get; set; }

        [JsonPropertyName("total fat in g")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal TotalFat { get; set; }

        [JsonPropertyName("saturated fat in g")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal SaturatedFat { get; set; }

        [JsonPropertyName("trans fat in g")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal TransFat { get; set; }

        [JsonPropertyName("cholesterol in mg")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal Cholesterol { get; set; }

        [JsonPropertyName("sodium in mg")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal Sodium { get; set; }

        [JsonPropertyName("total carbohydrate in g")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal Carbohydrates { get; set; }

        [JsonPropertyName("dietary fiber in g")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal Fiber { get; set; }

        [JsonPropertyName("total sugars in g")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal TotalSugars { get; set; }

        [JsonPropertyName("added sugars in g")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal AddedSugars { get; set; }

        [JsonPropertyName("protein in g")]
        [JsonConverter(typeof(DecimalStringFlexibleConverter))]
        public decimal Protein { get; set; }

        [JsonPropertyName("reasoning")]
        public string Reasoning { get; set; }
    }


}
