using ITIGraduationProject.BL.DTO.BlogPostManger;
using ITIGraduationProject.BL.DTO.RecipeManger;
using ITIGraduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.Manger
{
    public class RecipeManger : IRecipeManger
    {
        private readonly IUnitOfWork unitOfWork;

        public RecipeManger(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

        public async Task<GeneralResult> AddAsync(RecipeDetailsDTO item)
        {
            try
            {
                var creator = new ApplicationUser()
                {
                    UserName = item.Author.UserName,
                    Email = item.Author.Email,
                };

                var recipe = new Recipe()
                {
                    Title = item.Title,
                    Creator= creator,
                    Instructions = item.Instructions,
                    PrepTime = item.PrepTime,
                    Description = item.Description,
                    CookingTime = item.CookingTime,
                    CuisineType = item.CuisineType,
                    CreatedAt = DateTime.Now,
                    Ratings = [],
                    Comments = [],
                    Categories = (ICollection<RecipeCategory>)item.CategoryNames,
                    RecipeIngredients = [],
                    
                    //RecipeIngredients = item.RecipeIngredients?.Select(i => new RecipeIngredient
                    //{
                    //    IngredientID = i.IngredientId,
                    //    Quantity = i.Quantity,
                    //    Unit = i.Unit
                    //}).ToList() ?? new List<RecipeIngredient>()
                };

                unitOfWork.RecipeRepository.Add(recipe);
                await unitOfWork.SaveChangesAsync();

                return new GeneralResult
                {
                    Success = true,
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
                        Message = $"Failed to add recipe: {ex.Message}"
                    }]
                };
            }
        }

        public async Task<GeneralResult> DeleteAsync(int id)
        {
            try
            {
                var recipe = await unitOfWork.RecipeRepository.GetByIdAsync(id);
                if (recipe == null)
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError
                        {
                            Message = "Recipe not found"
                        }]
                    };

                unitOfWork.RecipeRepository.Delete(recipe);
                await unitOfWork.SaveChangesAsync();

                return new GeneralResult 
                { 
                    Success = true,
                    Errors = [new ResultError
                    {
                        Message = "Recipe deleted successfully"
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
                        Message = $"Failed to delete recipe: {ex.Message}"
                    }]
                };
            }
        }

        public async Task<List<RecipeDetailsDTO>> GetAll()
        {
            var recipes = await unitOfWork.RecipeRepository.GetAll();
            return recipes.Select(r => new RecipeDetailsDTO
            {
                RecipeID = r.RecipeID,
                Title = r.Title,
                Description = r.Description,
                Instructions = r.Instructions,
                PrepTime = r.PrepTime,
                CookingTime = r.CookingTime,
                CuisineType = r.CuisineType,
                CreatedAt = r.CreatedAt,
                Author = new AuthorNestedDTO
                {
                    UserName = r.Creator?.UserName,
                    Email = r.Creator?.Email,
                },
                CategoryNames = r.Categories?.Select(c => c.Category?.Name).ToList() ?? new List<string>(),
                Comments = r.Comments?.Select(c => new CommentNestedDTO
                {
                    CommentID = c.CommentID,
                    Content = c.Text,
                    CreatedAt = c.CreatedAt,
                    //User = new AuthorNestedDTO
                    //{
                    //    UserName = c.User?.UserName,
                    //    Email = c.User?.Email,
                    //}
                }).ToList() ?? new List<CommentNestedDTO>(),
                Ratings = r.Ratings?.Select(r => new RatingDTO
                {
                    Score = r.Score,
                }).ToList() ?? new List<RatingDTO>(),
                //    RecipeIngredients = r.RecipeIngredients?.Select(ri => new RecipeIngredientDTO
                //    {
                //        IngredientId = ri.IngredientId,
                //        Name = ri.Ingredient?.Name,
                //        Quantity = ri.Quantity,
                //        Unit = ri.Unit
                //    }).ToList() ?? new List<RecipeIngredientDTO>(),
                //    Ingredients = r.RecipeIngredients?.Select(ri => new IngredientDTO
                //    {
                //        IngredientId = ri.IngredientId,
                //        Name = ri.Ingredient?.Name,
                //        Quantity = ri.Quantity,
                //        Unit = ri.Unit
                //    }).ToList() ?? new List<IngredientDTO>()
                //
            }).ToList();

        }

        public Task<List<RecipeDetailsDTO>> GetByCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RecipeDetailsDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<RecipeDetailsDTO> GetByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResult> UpdateAsync(RecipeDetailsDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
