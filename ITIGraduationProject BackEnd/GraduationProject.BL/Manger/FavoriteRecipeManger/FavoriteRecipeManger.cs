using ITIGraduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class FavoriteRecipeManager : IFavoriteRecipeManger
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteRecipeManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GeneralResult> AddToFavoritesAsync(FavoriteRecipeCreateDto dto)
        {
            var exists = await _unitOfWork.FavoriteRecipeRepository.FindAsync(dto.UserID, dto.RecipeID);
            if (exists != null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError { Code = "AlreadyExists", Message = "Recipe is already in favorites." }]
                };
            }

            var favorite = new FavoriteRecipe
            {
                RecipeID = dto.RecipeID,
                UserID = dto.UserID
            };

             await _unitOfWork.FavoriteRecipeRepository.AddAsync(favorite);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult
            {
                Success = true,
                
            };
        }

        public async Task<GeneralResult<List<FavoriteRecipeDto>>> GetFavoritesForUser(int userId)
        {
            var favorites = await _unitOfWork.FavoriteRecipeRepository.GetFavoritesByUserId(userId);

            var dtos = favorites.Select(f => new FavoriteRecipeDto
            {
                
                RecipeID = f.RecipeID,
                RecipeTitle = f.Title ?? "",
                UserID = f.CreatedBy
            }).ToList();

            return new GeneralResult<List<FavoriteRecipeDto>>
            {
                Success = true,
                Data = dtos
            };
        }

        public async Task<GeneralResult> RemoveFromFavoritesAsync(int userId, int recipeId)
        {
            var favorite = await _unitOfWork.FavoriteRecipeRepository.FindAsync(userId, recipeId);
            if (favorite == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError { Code = "NotFound", Message = "Favorite recipe not found." }]
                };
            }

           await _unitOfWork.FavoriteRecipeRepository.RemoveAsync(favorite);
            await _unitOfWork.SaveChangesAsync();

            return new GeneralResult
            {
                Success = true,
               
            };
        }
    }

}
