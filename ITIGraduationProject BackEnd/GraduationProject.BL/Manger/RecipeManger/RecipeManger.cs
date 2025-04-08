using ITIGraduationProject.BL.DTO.RecipeManger.Input;
using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Identity;
using ITIGraduationProject.BL.DTO.RecipeManger.Output;
using ITIGraduationProject.BL.DTO.RecipeManger.Read;

namespace ITIGraduationProject.BL.Manger;

public class RecipeManger : IRecipeManger
{
    private readonly IUnitOfWork unitOfWork;
    private readonly UserManager<ApplicationUser> userManager;

    public RecipeManger(IUnitOfWork _unitOfWork, UserManager<ApplicationUser> _userManager)
    {
        unitOfWork = _unitOfWork;
        userManager = _userManager;
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
}