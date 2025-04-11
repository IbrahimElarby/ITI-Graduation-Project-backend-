using ITIGraduationProject.BL;
using ITIGraduationProject.BL.DTO.RecipeManger.Read;
using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    internal class IngredientManger : IIngredientManger
    {
        private readonly IUnitOfWork unitOfWork;

        public IngredientManger(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<List<IngredientDto>> GetAll()
        {
            var ingredients = await unitOfWork.IngredientRepository.GetAll();
            if (ingredients == null)
            {
                return null;
            }
            return ingredients.Select(i => new IngredientDto
            {
                IngredientID = i.IngredientID,
                Name = i.Name,
                CaloriesPer100g = i.CaloriesPer100g,
                Carbs   = i.Carbs,
                Fats = i.Fats,
                Protein = i.Protein,
            }).ToList();
        }

        public async Task<IngredientDto> GetById(int id)
        {
            var ingredient = await unitOfWork.IngredientRepository.GetByIdAsync(id);
            if (ingredient == null)
            {
                return null;
            }
            return new IngredientDto
            {
                IngredientID = ingredient.IngredientID,
                Name = ingredient.Name,
                CaloriesPer100g = ingredient.CaloriesPer100g,
                Carbs = ingredient.Carbs,
                Fats = ingredient.Fats,
                Protein = ingredient.Protein,
            };
        }
        public async Task<GeneralResult> AddAsync(IngredientDto item)
        {
            try
            {
                if (item == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError { Code = "NullInput", Message = "Ingredient cannot be null" }]
                    };
                }
                var ingrdient = new Ingredient
                {
                    IngredientID = item.IngredientID,
                    CaloriesPer100g = item.CaloriesPer100g,
                    Carbs = item.Carbs,
                    Fats = item.Fats,
                    Protein = item.Protein,
                    Name = item.Name,
                };
                unitOfWork.IngredientRepository.Add(ingrdient);
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
                Message = $"Failed to save ingredient: {ex.InnerException?.Message ?? ex.Message}"
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
        

        public async Task<GeneralResult> DeleteAsync(int id)
        {
            try
            {
                var ingredient = await unitOfWork.IngredientRepository.GetByIdAsync(id);
                if (ingredient == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError { Code = "IngredientNotFound", Message = "Ingredient not found" }]
                    };
                }
                 unitOfWork.IngredientRepository.Delete(ingredient);
                await unitOfWork.SaveChangesAsync();
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
            Message = $"Failed to save Ingredient: {ex.InnerException?.Message ?? ex.Message}"
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
            Code = "UnexpectedError",
            Message = $"Unexpected error: {ex.Message}"
        }]
                };
            }
        }


        public async Task<GeneralResult> UpdateAsync(IngredientDto item)
        {
            try
            { 
            if (item == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError { Code = "NullInput", Message = "Ingreident cannot be null" }]
                };
                
            }
            var ingredient = await unitOfWork.IngredientRepository.GetByIdAsync(item.IngredientID);
            if (ingredient == null)
            {
                return new GeneralResult
                {
                    Success = false,
                    Errors = [new ResultError { Code = "IngridentNotFound", Message = "Ingridient not found" }]
                };
            }
            ingredient.Name = item.Name;
            ingredient.Carbs = item.Carbs;
            ingredient.CaloriesPer100g = item.CaloriesPer100g;
            ingredient.Fats = item.Fats;
            ingredient.Protein = item.Protein;
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
            Message = $"Failed to save Category: {ex.InnerException?.Message ?? ex.Message}"
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
            Code = "UnexpectedError",
            Message = $"Unexpected error: {ex.Message}"
        }]
                       };
            }
        }
    }
}
