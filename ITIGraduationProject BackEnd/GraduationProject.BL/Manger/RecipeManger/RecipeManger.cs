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
    private readonly NutritionService _nutritionService;

    public RecipeManger(IUnitOfWork _unitOfWork, UserManager<ApplicationUser> _userManager, ExternalRecipeService externalRecipeService , NutritionService nutritionService)
    {
        unitOfWork = _unitOfWork;
        userManager = _userManager;
        _externalRecipeService = externalRecipeService;
        _nutritionService = nutritionService;
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

    private RecipeDetailsDTO ToRecipeDetailsDTO(Recipe recipe)
    {
        return new RecipeDetailsDTO
        {
            RecipeID = recipe.RecipeID,
            Title = recipe.Title,
            Instructions = recipe.Instructions,
            PrepTime = recipe.PrepTime,
            Calories = recipe.Calories,
            Protein = recipe.Protein,
            Carbs = recipe.Carbs,
            Fats = recipe.Fats,
            Description = recipe.Description,
            CookingTime = recipe.CookingTime,
            CuisineType = recipe.CuisineType,
            CreatedAt = recipe.CreatedAt,
            Author = new AuthorNestedDTO
            {
                Id = recipe.CreatedBy.ToString(),
                UserName = recipe.Creator?.UserName
            },
            CreatorName = recipe.Creator?.UserName,
            Ingredients = recipe.RecipeIngredients?.Select(ri => new RecipeIngredientDto
            {
                IngredientName = ri.Ingredient?.Name,
                Quantity = ri.Quantity,
                Unit = ri.Unit,
                CaloriesPer100g = ri.Ingredient?.CaloriesPer100g ?? 0
            }).ToList() ?? new List<RecipeIngredientDto>(),

            Ratings = recipe.Ratings?.Select(r => new RatingDTO
            {
                Score = r.Score,
                
            }).ToList() ?? new List<RatingDTO>(),

            Comments = recipe.Comments?.Select(c => new CommentNestedDTO
            {
                CommentID = c.CommentID,
                Content = c.Text,
                CreatedAt = c.CreatedAt,
                
                
            }).ToList() ?? new List<CommentNestedDTO>(),

            CategoryNames = recipe.Categories?.Select(rc => rc.Category?.Name).ToList() ?? new List<string>()
        };
    }


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
        var recipeNutrition = await _nutritionService.GetNutritionAsync(aiRecipe.Title);

        

       
       

        var recipe = new Recipe
        {
            Title = aiRecipe.Title,
            Instructions = string.Join(". ", aiRecipe.Instructions),
            Description = $"AI generated {input.MealType} for {string.Join(", ", input.DietaryRestrictions)} diet",
            Calories = recipeNutrition?.Calories ?? 0, 
            Protein= recipeNutrition?.Protein?? 0,
            Carbs = recipeNutrition?.Carbohydrates ?? 0,
            Fats = recipeNutrition?.Fat ?? 0,
            CuisineType = input.Cuisine,
            CreatedAt = DateTime.UtcNow,

            CreatedBy = 8, // TODO: Replace with current user ID
            RecipeIngredients = new List<RecipeIngredient>()
        };

        var ingredientDtos = new List<RecipeIngredientDto>();

        foreach (var aiIng in aiRecipe.Ingredients)
        {
            var nutrition = await _nutritionService.GetNutritionAsync(aiIng.Name);
           
            var existing = await unitOfWork.IngredientRepository.FindByName(aiIng.Name);
            if (existing == null)
            {
                existing = new Ingredient
                {
                    Name = aiIng.Name,
                    CaloriesPer100g = nutrition?.Calories??0,
                    Protein = nutrition?.Protein ?? 0,
                    Carbs = nutrition?.Carbohydrates ?? 0,
                    Fats = nutrition?.Fat ?? 0
                };

                unitOfWork.IngredientRepository.Add(existing);
                await unitOfWork.SaveChangesAsync();
            }
            else
            {
                existing.Protein = nutrition?.Protein ?? 0;
                existing.CaloriesPer100g = nutrition?.Calories ?? 0;
                existing.Carbs = nutrition?.Carbohydrates ?? 0;
                existing.Fats = nutrition?.Fat ?? 0;
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