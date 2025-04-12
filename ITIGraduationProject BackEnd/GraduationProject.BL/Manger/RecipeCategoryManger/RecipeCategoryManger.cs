using ITIGraduationProject.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class RecipeCategoryManger : IRecipeCategoryManger
    {
        private readonly IUnitOfWork unitOfWork;

        public RecipeCategoryManger(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<GeneralResult> AssignCategory(int recipeId, int categoryId)
        {
            try
            {
                // Validate existence of BlogPost and Category
                var RecipeExists = await unitOfWork.RecipeRepository.GetByIdAsync(recipeId);
                var categoryExists = await unitOfWork.CategoryRepository.GetByIdAsync(categoryId);

                if (RecipeExists == null || categoryExists == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError
                    {
                        Code = "NotFound",
                        Message = "Recipe or Category does not exist."
                    }]
                    };
                }
                var alreadyLinked = await unitOfWork.RecipeCategoryRepository.IsAssigned(recipeId, categoryId);
                if (alreadyLinked)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError
                    {
                        Code = "AlreadyExists",
                        Message = "This category is already linked to the recipe."
                    }]
                    };
                }
                var Recipecategory = new RecipeCategory
                {
                    RecipeID = recipeId,
                    CategoryID = categoryId,
                };
                unitOfWork.RecipeCategoryRepository.Add(Recipecategory);
                var saveResult = await unitOfWork.SaveChangesAsync();
                return saveResult > 0
                    ? new GeneralResult { Success = true }
                    : new GeneralResult { Success = false, Errors = [new ResultError { Code = "SaveFailed", Message = "No changes persisted" }] };
            }
            catch (DbUpdateException ex)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError
            {
                Code = "DatabaseError",
                Message = $"Failed to save assign category: {ex.InnerException?.Message ?? ex.Message}"
            }]
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError
            {
                Code = "AddFailed",
                Message = $"Unexpected error: {ex.Message}"
            }]
                };
            }
        }


        public async Task<GeneralResult> UnAssignCategory(int recipeId, int categoryId)
        {
            try
            {
                // 1. Ensure the relationship exists
                var recipeCategory = await unitOfWork.RecipeCategoryRepository
                    .GetByCompositeKeyAsync(recipeId, categoryId);

                if (recipeCategory == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError
                {
                    Code = "NotFound",
                    Message = "This category is not assigned to the recipe."
                }]
                    };
                }

                // 2. Remove the link
                unitOfWork.RecipeCategoryRepository.Delete(recipeCategory);

                // 3. Save changes
                var result = await unitOfWork.SaveChangesAsync();

                return result > 0
                    ? new GeneralResult { Success = true }
                    : new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError
                {
                    Code = "SaveFailed",
                    Message = "No changes were saved during unassignment."
                }]
                    };
            }
            catch (DbUpdateException ex)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError
            {
                Code = "DatabaseError",
                Message = $"EF error during delete: {ex.InnerException?.Message ?? ex.Message}"
            }]
                };
            }
            catch (Exception ex)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError
            {
                Code = "ServerError",
                Message = $"Unexpected error: {ex.Message}"
            }]
                };
            }
        }

    }

}
