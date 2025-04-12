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
    public class BlogPostCategoryManger : IBlogPostCategoryManger
    {
        private readonly IUnitOfWork unitOfWork;

        public BlogPostCategoryManger(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<GeneralResult> AssignCategory(int blogPostId, int categoryId)
        {
            try
            {
                // Validate existence of BlogPost and Category
                var blogPostExists = await unitOfWork.PostBlogRepository.GetByIdAsync(blogPostId);
                var categoryExists = await unitOfWork.CategoryRepository.GetByIdAsync(categoryId);

                if (blogPostExists == null || categoryExists == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError
                    {
                        Code = "NotFound",
                        Message = "BlogPost or Category does not exist."
                    }]
                    };
                }
                var alreadyLinked = await unitOfWork.PostCategoryRepository.IsAssigned(blogPostId, categoryId);
                if (alreadyLinked)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError
                    {
                        Code = "AlreadyExists",
                        Message = "This category is already linked to the blog post."
                    }]
                    };
                }
                var blogpostcategory = new BlogPostCategory
                {
                    BlogPostID = blogPostId,
                    CategoryID = categoryId,
                };
                unitOfWork.PostCategoryRepository.Add(blogpostcategory);
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


        public async Task<GeneralResult> UnAssignCategory(int blogPostId, int categoryId)
        {
            try
            {
                // 1. Ensure the relationship exists
                var blogPostCategory = await unitOfWork.PostCategoryRepository
                    .GetByCompositeKeyAsync(blogPostId, categoryId);

                if (blogPostCategory == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError
                {
                    Code = "NotFound",
                    Message = "This category is not assigned to the blog post."
                }]
                    };
                }

                // 2. Remove the link
                unitOfWork.PostCategoryRepository.Delete(blogPostCategory);

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
