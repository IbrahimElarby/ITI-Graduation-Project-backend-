using ITIGraduationProject.BL.DTO.RecipeManger.Input;
using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Identity;
using ITIGraduationProject.BL.DTO.RecipeManger.Output;
using ITIGraduationProject.BL.DTO.RecipeManger.Read;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ITIGraduationProject.BL.Manger;

public class RecipeManger : IRecipeManger
{
    private readonly IUnitOfWork unitOfWork;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ExternalRecipeService _externalRecipeService;

    public RecipeManger(IUnitOfWork _unitOfWork, UserManager<ApplicationUser> _userManager, ExternalRecipeService externalRecipeService)
    {
        unitOfWork = _unitOfWork;
        userManager = _userManager;
        _externalRecipeService = externalRecipeService;
    }

    public async Task<GeneralResult> AddAsync(RecipeCreateDto item)
    {
        if (item == null)
        {
            return new GeneralResult
            {
                Success = false,
                Errors = [new ResultError { Code = "NullInput", Message = "Recipe cannot be null" }]
            };
        }
        var author = await userManager.FindByIdAsync(item.Author.Id);
        if (author == null)
        {
            return new GeneralResult
            {
                Success = false,
                Errors = [new ResultError { Code = "UserNotFound", Message = "User not found" }]
            };
        }

        var recipe = new Recipe
        {
            Title = item.Title,
            Instructions = item.Instructions,
            PrepTime = item.PrepTime,
            Description = item.Description,
            CookingTime = item.CookingTime,
            CuisineType = item.CuisineType,
            CreatedAt = DateTime.Now,
            Creator = author,
            CreatedBy = author.Id,
            Categories = new List<RecipeCategory>(),
            RecipeIngredients = new List<RecipeIngredient>()
        };

        if (item.CategoryNames != null)
        {
            foreach (var categoryName in item.CategoryNames)
            {
                var category = await unitOfWork.CategoryRepository.GetByName(categoryName);
                if (category != null)
                {
                    recipe.Categories.Add(new RecipeCategory { Recipe = recipe, Category = category });
                }
            }
        }
        if (item.Ingredients != null)
        {
            foreach (var ingredientDto in item.Ingredients)
            {
                var ingredient = await unitOfWork.IngredientRepository.GetByIdAsync(ingredientDto.IngredientID);
                if (ingredient != null)
                {
                    recipe.RecipeIngredients.Add(new RecipeIngredient
                    {
                        Recipe = recipe,
                        Ingredient = ingredient,
                        Quantity = ingredientDto.Quantity,
                        Unit = ingredientDto.Unit
                    });
                }
            }
        }
        unitOfWork.RecipeRepository.Add(recipe);
        await unitOfWork.SaveChangesAsync();

        return new GeneralResult { Success = true };
    }

    public async Task<GeneralResult> DeleteAsync(int id)
    {
        var recipe = await unitOfWork.RecipeRepository.GetByIdAsync(id);
        if (recipe == null)
        {
            return new GeneralResult
            {
                Success = false,
                Errors = [new ResultError { Code = "RecipeNotFound", Message = "Recipe Not Found" }]
            };
        }

        unitOfWork.RecipeRepository.Delete(recipe);
        await unitOfWork.SaveChangesAsync();

        return new GeneralResult { Success = true };
    }

    public async Task<List<RecipeDetailsDTO>> GetAll()
    {
        var recipes = await unitOfWork.RecipeRepository.GetAll();
        return recipes.Select(ToRecipeDetailsDTO).ToList();
    }

    public async Task<List<RecipeDetailsDTO>> GetByCategory(int id)
    {
        var recipes = await unitOfWork.RecipeRepository.GetByCategory(id);
        return recipes.Select(ToRecipeDetailsDTO).ToList();
    }

    public async Task<RecipeDetailsDTO?> GetById(int id)
    {
        var recipe = await unitOfWork.RecipeRepository.GetByIdAsync(id);
        return recipe == null ? null : ToRecipeDetailsDTO(recipe);
    }

    public async Task<List<RecipeDetailsDTO>> GetByTitle(string title)
    {
        var recipes = await unitOfWork.RecipeRepository.GetByTitle(title);
        return recipes?.Select(ToRecipeDetailsDTO).ToList() ?? new List<RecipeDetailsDTO>();
    }

    public async Task<GeneralResult> UpdateAsync(RecipeDetailsDTO item)
    {
        var recipe = await unitOfWork.RecipeRepository.GetByIdAsync(item.RecipeID);
        if (recipe == null)
        {
            return new GeneralResult
            {
                Success = false,
                Errors = [new ResultError { Code = "RecipeNotFound", Message = "Recipe Not Found" }]
            };
        }

        recipe.Title = item.Title;
        recipe.Instructions = item.Instructions;
        recipe.PrepTime = item.PrepTime;
        recipe.Description = item.Description;
        recipe.CookingTime = item.CookingTime;
        recipe.CuisineType = item.CuisineType;

        unitOfWork.RecipeRepository.Update(recipe);
        await unitOfWork.SaveChangesAsync();

        return new GeneralResult { Success = true };
    }

    private RecipeDetailsDTO ToRecipeDetailsDTO(Recipe r) => new()
    {
        RecipeID = r.RecipeID,
        Title = r.Title,
        Description = r.Description,
        Instructions = r.Instructions,
        PrepTime = r.PrepTime,
        CookingTime = r.CookingTime,
        CuisineType = r.CuisineType,
        CreatedAt = r.CreatedAt,
        Author = new AuthorNestedDTO { UserName = r.Creator?.UserName },
        CategoryNames = r.Categories?.Select(c => c.Category?.Name).ToList() ?? new List<string>(),
        Comments = r.Comments?.Select(c => new CommentNestedDTO
        {
            CommentID = c.CommentID,
            Content = c.Text,
            CreatedAt = c.CreatedAt
        }).ToList() ?? new List<CommentNestedDTO>(),
        Ratings = r.Ratings?.Select(rt => new RatingDTO
        {
            Score = rt.Score
        }).ToList() ?? new List<RatingDTO>()

    };

    public async Task<GeneralResult<RecipeDetailsDTO>> ImportAndSaveRecipe(AiRecipeRequest input)
    {
        var aiRecipe = await _externalRecipeService.GenerateRecipeAsync(input);
        if (aiRecipe == null)
        {
            return new GeneralResult<RecipeDetailsDTO>
            {
                Success = false,
                Errors = [new ResultError { Code = "APIError", Message = "Failed to fetch recipe from AI API." }]
            };
        }

        // Parse total nutrition
        var totalCalories = aiRecipe.Nutrition.Calories;
        var totalProtein = decimal.TryParse(aiRecipe.Nutrition.Protein, out var p) ? p : 0; ;
        var totalCarbs = decimal.TryParse(aiRecipe.Nutrition.Carbohydrates, out var c) ? c : 0;
        var totalFat = decimal.TryParse(aiRecipe.Nutrition.Fat, out var f) ? f : 0;

       
        int count = aiRecipe.Ingredients.Count;
        decimal avgCalories = totalCalories / count;
        decimal avgProtein = totalProtein / count;
        decimal avgCarbs = totalCarbs / count;
        decimal avgFat = totalFat / count;

        var recipe = new Recipe
        {
            Title = aiRecipe.Title,
            Instructions = string.Join(". ", aiRecipe.Instructions),
            Description = $"AI generated {input.MealType} for {string.Join(", ", input.DietaryRestrictions)} diet",
            Calories = totalCalories,
            CuisineType = input.Cuisine,
            CreatedAt = DateTime.UtcNow,

            CreatedBy = 8, // TODO: Replace with current user ID
            RecipeIngredients = new List<RecipeIngredient>()
        };

        var ingredientDtos = new List<RecipeIngredientDto>();

        foreach (var aiIng in aiRecipe.Ingredients)
        {
            var existing = await unitOfWork.IngredientRepository.FindByName(aiIng.Name);
            if (existing == null)
            {
                existing = new Ingredient
                {
                    Name = aiIng.Name,
                    CaloriesPer100g = avgCalories,
                    Protein = avgProtein,
                    Carbs = avgCarbs,
                    Fats = avgFat
                };

                unitOfWork.IngredientRepository.Add(existing);
                await unitOfWork.SaveChangesAsync();
            }

            var (quantity, unit) = ParseQuantityAndUnit(aiIng.Quantity);

            recipe.RecipeIngredients.Add(new RecipeIngredient
            {
                IngredientID = existing.IngredientID,
                Quantity = quantity,
                Unit = unit
            });

            ingredientDtos.Add(new RecipeIngredientDto
            {
                IngredientName = existing.Name,
                Quantity = quantity,
                Unit = unit
            });
        }

        unitOfWork.RecipeRepository.Add(recipe);
        await unitOfWork.SaveChangesAsync();

        return new GeneralResult<RecipeDetailsDTO>
        {
            Success = true,
            Data = new RecipeDetailsDTO
            {
                RecipeID = recipe.RecipeID,
                Title = recipe.Title,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                Calories = recipe.Calories,
                CreatedAt = recipe.CreatedAt,
                CuisineType = recipe.CuisineType,
                Ingredients = ingredientDtos
            }
        };
    }


    private (decimal quantity, string unit) ParseQuantityAndUnit(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return (100, "g");

        input = input.Trim().ToLower();

        if (Regex.IsMatch(input, @"(to taste|some|few|pinch|as needed)"))
            return (1, input); // fallback descriptive unit

        var match = Regex.Match(input, @"(\d+(\.\d+)?)\s*(\w+)?");
        if (match.Success)
        {
            decimal qty = decimal.TryParse(match.Groups[1].Value, out var q) ? q : 1;
            string unit = match.Groups[3].Success ? match.Groups[3].Value : "unit";
            return (qty, unit);
        }

        return (1, input); // final fallback
    }





}