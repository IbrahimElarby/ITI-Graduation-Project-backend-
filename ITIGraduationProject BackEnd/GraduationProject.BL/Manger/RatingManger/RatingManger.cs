using ITIGraduationProject.BL.DTO.RecipeManger.Output;
using ITIGraduationProject.BL.DTO.RecipeManger.Read;
using ITIGraduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class RatingManger : IRatingManger
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public RatingManger( IUnitOfWork unitOfWork)
        {
           
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResult> AddRatingAsync(RatingDto dto, int userId)
        {
            var recipe = await _unitOfWork.RecipeRepository.GetByIdAsync(dto.RecipeId);
            if (recipe == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError { Code = "NotFound", Message = "Recipe not found" }]
                };
            }

            var rating = new Rating
            {
                Score = dto.Score,
                UserID = userId,
                RecipeID = dto.RecipeId
            };

            await _unitOfWork.RatingRepository.AddAsync(rating);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.RatingRepository.UpdateRecipeAverageRatingAsync(dto.RecipeId);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult { Success = true };
        }

        public async Task<GeneralResult<List<RatingDto>>> GetRatingsByRecipeAsync(int recipeId)
        {
            var ratings = await _unitOfWork.RatingRepository.GetRatingsByRecipeIdAsync(recipeId);

            var dtos = ratings.Select(r => new RatingDto
            {
                Score = r.Score,
                RecipeId = r.RecipeID
            }).ToList();

            return new GeneralResult<List<RatingDto>> { Success = true, Data = dtos };
        }

        public async Task<GeneralResult<List<RecipeDetailsDTO>>> GetTopRatedRecipesAsync(int count = 9)
        {
            var recipes = await _unitOfWork.RatingRepository.GetTopRatedRecipesAsync(count);

            var dtos = recipes.Select(r => new RecipeDetailsDTO
            {
                RecipeID = r.RecipeID,
                Title = r.Title ?? "",
                Calories = r.Calories,
                Protein = r.Protein,
                Carbs = r.Carbs,
                Image = r.Image,
                Fats = r.Fats,
                CuisineType = r.CuisineType ?? "",
                AvgRating = r.AvgRating,
                Instructions = r.Instructions ?? "",
                CreatedAt = r.CreatedAt,
                Ingredients = r.RecipeIngredients?.Select(i => new RecipeIngredientDto
                {
                    IngredientName = i.Ingredient?.Name ?? "",
                    Quantity = i.Quantity,
                    Unit = i.Unit
                }).ToList() ?? []
            }).ToList();

            return new GeneralResult<List<RecipeDetailsDTO>> { Success = true, Data = dtos };
        }
    }
}
