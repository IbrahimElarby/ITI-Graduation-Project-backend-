using ITIGraduationProject.BL.DTO.Category;
using ITIGraduationProject.BL.DTO.RecipeManger.Read;
using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.Manger.CategoryManger
{
    public class CategoryManger : ICategoryManger
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        public CategoryManger(IUnitOfWork _unitOfWork, UserManager<ApplicationUser> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
        }
        public async Task<List<CategoryDetailsDTO>> GetAll()
        {
            var categoriesFromDb = await unitOfWork.CategoryRepository.GetAll();

            if (categoriesFromDb == null)
                return null;

            var result = categoriesFromDb.Select(c => new CategoryDetailsDTO
            {
                CategoryID = c.CategoryID,
                Name = c.Name,

                BlogPosts = c.BlogPosts.Select(bp => new BlogPostNestedDto
                {
                    BlogPostID = bp.BlogPostID,
                    Title = bp.BlogPost?.Title ?? "",
                    Content = bp.BlogPost?.Content ?? "",
                    FeaturedImageUrl = bp.BlogPost?.FeaturedImageUrl ?? "",
                    CreatedAt = bp.BlogPost?.CreatedAt ?? DateTime.MinValue,
                    Author = bp.BlogPost?.Author != null
                        ? new AuthorNestedDTO
                        {
                            
                            UserName = bp.BlogPost.Author.UserName

                            // Fill your nested author DTO fields here
                        }
                        : null,
                    Comments = bp.BlogPost?.Comments?.Select(comment => new CommentNestedDTO
                    {
                        // Fill your comment fields here
                    }).ToList() ?? new List<CommentNestedDTO>()
                }).ToList(),

                Recipes = c.Recipes?.Select(rc => new RecipeNestedDTO
                {
                    RecipeID = rc.RecipeID,
                    Title = rc.Recipe?.Title ?? "",
                    Instructions = rc.Recipe?.Instructions ?? "",
                    PrepTime = rc.Recipe?.PrepTime ?? 0,
                    Description = rc.Recipe?.Description ?? "",
                    CookingTime = rc.Recipe?.CookingTime ?? 0,
                    CuisineType = rc.Recipe?.CuisineType ?? "",
                    CreatedAt = rc.Recipe?.CreatedAt ?? DateTime.MinValue,
                    CreatorName = rc.Recipe?.Creator.UserName ?? "",
                    Ingredients = rc.Recipe?.RecipeIngredients?.Select(i => new RecipeIngredientDto
                    {
                        IngredientID = i.IngredientID,
                        IngredientName = i.Ingredient.Name,
                        CaloriesPer100g = i.Ingredient.CaloriesPer100g,
                        Protein = i.Ingredient.Protein,
                        Quantity = i.Quantity,
                        Unit = i.Unit
        
                        // Fill ingredient fields here
                    }).ToList() ?? new List<RecipeIngredientDto>(),
                    Ratings = rc.Recipe?.Ratings?.Select(r => new RatingDTO
                    {
                        Score = r.Score,
                        // Fill rating fields here
                    }).ToList() ?? new List<RatingDTO>(),
                    Comments = rc.Recipe?.Comments?.Select(cmnt => new CommentNestedDTO
                    {
                        CommentID = cmnt.CommentID,
                        Content = cmnt.Text,
                        CreatedAt = cmnt.CreatedAt
                        // Fill comment fields here
                    }).ToList() ?? new List<CommentNestedDTO>()
                }).ToList() ?? new List<RecipeNestedDTO>()
            }).ToList();

            return result;
        }


        public async Task<CategoryDetailsDTO?> GetById(int id)
        {
            var categoryFromDb = await unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (categoryFromDb == null)
                return null;

            return new CategoryDetailsDTO
            {
                CategoryID = categoryFromDb.CategoryID,
                Name = categoryFromDb.Name,

                BlogPosts = categoryFromDb.BlogPosts.Select(bp => new BlogPostNestedDto
                {
                    BlogPostID = bp.BlogPostID,
                    Title = bp.BlogPost?.Title ?? "",
                    Content = bp.BlogPost?.Content ?? "",
                    FeaturedImageUrl = bp.BlogPost?.FeaturedImageUrl ?? "",
                    CreatedAt = bp.BlogPost?.CreatedAt ?? DateTime.MinValue,
                    Author = bp.BlogPost?.Author != null
                        ? new AuthorNestedDTO
                        {
                           
                            UserName = bp.BlogPost.Author.UserName
                        }
                        : null,
                    Comments = bp.BlogPost?.Comments?.Select(comment => new CommentNestedDTO
                    {
                        CommentID = comment.CommentID,
                        Content = comment.Text,
                        CreatedAt = comment.CreatedAt
                    }).ToList() ?? new List<CommentNestedDTO>()
                }).ToList(),

                Recipes = categoryFromDb.Recipes?.Select(rc => new RecipeNestedDTO
                {
                    RecipeID = rc.RecipeID,
                    Title = rc.Recipe?.Title ?? "",
                    Instructions = rc.Recipe?.Instructions ?? "",
                    PrepTime = rc.Recipe?.PrepTime ?? 0,
                    Description = rc.Recipe?.Description ?? "",
                    CookingTime = rc.Recipe?.CookingTime ?? 0,
                    CuisineType = rc.Recipe?.CuisineType ?? "",
                    CreatedAt = rc.Recipe?.CreatedAt ?? DateTime.MinValue,
                    CreatorName = rc.Recipe?.Creator?.UserName ?? "",
                    
                    Ingredients = rc.Recipe?.RecipeIngredients?.Select(i => new RecipeIngredientDto
                    {
                        IngredientID = i.IngredientID,
                        IngredientName = i.Ingredient.Name,
                        CaloriesPer100g = i.Ingredient.CaloriesPer100g,
                        Protein = i.Ingredient.Protein,
                        Quantity = i.Quantity,
                        Unit = i.Unit
                    }).ToList() ?? new List<RecipeIngredientDto>(),
                    Ratings = rc.Recipe?.Ratings?.Select(r => new RatingDTO
                    {
                        Score = r.Score
                    }).ToList() ?? new List<RatingDTO>(),
                    Comments = rc.Recipe?.Comments?.Select(cmnt => new CommentNestedDTO
                    {
                        CommentID = cmnt.CommentID,
                        Content = cmnt.Text,
                        CreatedAt = cmnt.CreatedAt
                    }).ToList() ?? new List<CommentNestedDTO>()
                }).ToList() ?? new List<RecipeNestedDTO>()
            };
        }

        public async Task<CategoryDetailsDTO?> GetByName(string name)
        {
            var categoryFromDb = await unitOfWork.CategoryRepository.GetByName(name);
            if (categoryFromDb == null)
                return null;

            return new CategoryDetailsDTO
            {
                CategoryID = categoryFromDb.CategoryID,
                Name = categoryFromDb.Name,

                BlogPosts = categoryFromDb.BlogPosts.Select(bp => new BlogPostNestedDto
                {
                    BlogPostID = bp.BlogPostID,
                    Title = bp.BlogPost?.Title ?? "",
                    Content = bp.BlogPost?.Content ?? "",
                    FeaturedImageUrl = bp.BlogPost?.FeaturedImageUrl ?? "",
                    CreatedAt = bp.BlogPost?.CreatedAt ?? DateTime.MinValue,
                    Author = bp.BlogPost?.Author != null
                        ? new AuthorNestedDTO
                        {
                            
                            UserName = bp.BlogPost.Author.UserName
                        }
                        : null,
                    Comments = bp.BlogPost?.Comments?.Select(comment => new CommentNestedDTO
                    {
                        CommentID = comment.CommentID,
                        Content = comment.Text,
                        CreatedAt = comment.CreatedAt
                    }).ToList() ?? new List<CommentNestedDTO>()
                }).ToList(),

                Recipes = categoryFromDb.Recipes?.Select(rc => new RecipeNestedDTO
                {
                    RecipeID = rc.RecipeID,
                    Title = rc.Recipe?.Title ?? "",
                    Instructions = rc.Recipe?.Instructions ?? "",
                    PrepTime = rc.Recipe?.PrepTime ?? 0,
                    Description = rc.Recipe?.Description ?? "",
                    CookingTime = rc.Recipe?.CookingTime ?? 0,
                    CuisineType = rc.Recipe?.CuisineType ?? "",
                    CreatedAt = rc.Recipe?.CreatedAt ?? DateTime.MinValue,
                    CreatorName = rc.Recipe?.Creator?.UserName ?? "",
                    
                    Ingredients = rc.Recipe?.RecipeIngredients?.Select(i => new RecipeIngredientDto
                    {
                        IngredientID = i.IngredientID,
                        IngredientName = i.Ingredient.Name,
                        CaloriesPer100g = i.Ingredient.CaloriesPer100g,
                        Protein = i.Ingredient.Protein,
                        Quantity = i.Quantity,
                        Unit = i.Unit
                    }).ToList() ?? new List<RecipeIngredientDto>(),
                    Ratings = rc.Recipe?.Ratings?.Select(r => new RatingDTO
                    {
                        Score = r.Score
                    }).ToList() ?? new List<RatingDTO>(),
                    Comments = rc.Recipe?.Comments?.Select(cmnt => new CommentNestedDTO
                    {
                        CommentID = cmnt.CommentID,
                        Content = cmnt.Text,
                        CreatedAt = cmnt.CreatedAt
                    }).ToList() ?? new List<CommentNestedDTO>()
                }).ToList() ?? new List<RecipeNestedDTO>()
            };
        }

        public async Task<GeneralResult> AddAsync(CategoryAddDTO item)
        {
            try
            {

                if (item == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError { Code = "NullInput", Message = "Blog post cannot be null" }]
                    };
                }
                var category = new Category
                {
                   
                    Name = item.Name,
                };
                unitOfWork.CategoryRepository.Add(category);

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
                Message = $"Failed to save blog post: {ex.InnerException?.Message ?? ex.Message}"
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

        public async Task<GeneralResult> UpdateAsync(CategoryAddDTO item)
        {
            try
            {
                if (item == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError { Code = "NullInput", Message = "Blog post cannot be null" }]
                    };
                }
                var category = await unitOfWork.CategoryRepository.GetByIdAsync(item.CategoryID);
                if (category == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError { Code = "CategoryNotFound", Message = "Category not found" }]
                    };
                }
                 category.Name = item.Name;
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
        public async Task<GeneralResult> DeleteAsync(int id)
        {
            try
            {
                var category = await unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError { Code = "CategoryNotFound", Message = "Category not found" }]
                    };
                }

                unitOfWork.CategoryRepository.Delete(category);



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
