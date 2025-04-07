using ITIGraduationProject.BL;
using ITIGraduationProject.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class BlogPostManger : IBlogPostManger
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        public BlogPostManger(IUnitOfWork _unitOfWork, UserManager<ApplicationUser> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
        }
        public async Task<List<BlogPostDetailsDTO>> GetAll()
        {
            var postsFromDb = await unitOfWork.PostBlogRepository.GetAll();

            if (postsFromDb == null)
            {
                return null;
            }

            return postsFromDb.Select(p => new BlogPostDetailsDTO
            {
                BlogPostID = p.BlogPostID,
                Title = p.Title,
                Content = p.Content,
                FeaturedImageUrl = p.FeaturedImageUrl,
                CreatedAt = p.CreatedAt,
                Author = new AuthorNestedDTO
                {
                    UserName = p.Author?.UserName ?? "",
                   
                },
                Comments = p.Comments?.Select(c => new CommentNestedDTO
                {
                    CommentID = c.CommentID,
                    Content = c.Text,
                    CreatedAt = c.CreatedAt
                }).ToList() ?? new List<CommentNestedDTO>(),
                CategoryNames = p.Categories?
                    .Select(bpc => bpc.Category?.Name ?? "Uncategorized")
                    .ToList() ?? new List<string>()
            }).ToList();
        }

        public async Task<BlogPostDetailsDTO> GetById(int id)
        {
            var p = await unitOfWork.PostBlogRepository.GetByIdAsync(id);
           if (p == null)
            {
                return null;
            }
            return new BlogPostDetailsDTO
            {
                BlogPostID = p.BlogPostID,
                Title = p.Title,
                Content = p.Content,
                FeaturedImageUrl = p.FeaturedImageUrl,
                CreatedAt = p.CreatedAt,
                Author = new AuthorNestedDTO
                {
                    UserName = p.Author?.UserName ?? "",
                   
                },
                Comments = p.Comments?.Select(c => new CommentNestedDTO
                {
                    CommentID = c.CommentID,
                    Content = c.Text,
                    CreatedAt = c.CreatedAt
                }).ToList() ?? new List<CommentNestedDTO>(),
                CategoryNames = p.Categories?
                    .Select(bpc => bpc.Category?.Name ?? "Uncategorized")
                    .ToList() ?? new List<string>()
            };
        }

        public async Task<List<BlogPostDetailsDTO>> GetByCategory(int id)
        {
            var postsFromDb = await unitOfWork.PostBlogRepository.GetByCategory(id);

            if (postsFromDb == null)
            {
                return null;
            }
            return postsFromDb.Select(p => new BlogPostDetailsDTO
            {
                BlogPostID = p.BlogPostID,
                Title = p.Title,
                Content = p.Content,
                FeaturedImageUrl = p.FeaturedImageUrl,
                CreatedAt = p.CreatedAt,
                Author = new AuthorNestedDTO
                {
                    UserName = p.Author?.UserName ?? "",

                },
                Comments = p.Comments?.Select(c => new CommentNestedDTO
                {
                    CommentID = c.CommentID,
                    Content = c.Text,
                    CreatedAt = c.CreatedAt
                }).ToList() ?? new List<CommentNestedDTO>(),
                CategoryNames = p.Categories?
                   .Select(bpc => bpc.Category?.Name ?? "Uncategorized")
                   .ToList() ?? new List<string>()
            }).ToList();

        }
        public async Task<GeneralResult> AddAsync(BlogPostAddDTO item)
        {
            try
            {
                // Validate input
                if (item == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError { Code = "NullInput", Message = "Blog post cannot be null" }]
                    };
                }

                // Get existing author (assuming author should already exist)
                var author = await userManager.FindByIdAsync(item.Author.Id);
                if (author == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError { Code = "AuthorNotFound", Message = "Specified author does not exist" }]
                    };
                }

                var post = new BlogPost
                {
                    Title = item.Title,
                    Content = item.Content,
                    FeaturedImageUrl = item.FeaturedImageUrl,
                    CreatedAt = DateTime.UtcNow,  // Typically set server-side
                    Author = author,
                    Categories = new List<BlogPostCategory>()
                    
                };

                // Handle categories
                if (item.CategoryNames?.Any() == true)
                {
                    foreach (var categoryname in item.CategoryNames)
                    {
                        var category = await unitOfWork.CategoryRepository.GetByName(categoryname);
                        if (category != null)
                        {
                            post.Categories.Add(new BlogPostCategory { Category = category });
                        }
                    }
                }
                
                unitOfWork.PostBlogRepository.Add(post);
                
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

        public async Task<GeneralResult> UpdateAsync(BlogPostUpdateDTO item)
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

                var post = await unitOfWork.PostBlogRepository.GetByIdAsync(item.BlogPostID);
                if (post == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError { Code = "PostNotFound", Message = "Blog post not found" }]
                    };
                }

                post.Content = item.Content;
                post.FeaturedImageUrl = item.FeaturedImageUrl;

                if (item.CategoryNames?.Any() == true)
                {
                    post.Categories ??= new List<BlogPostCategory>();
                    post.Categories.Clear(); // Optional: replace old categories

                    foreach (var categoryName in item.CategoryNames)
                    {
                        var category = await unitOfWork.CategoryRepository.GetByName(categoryName);
                        if (category != null)
                        {
                            post.Categories.Add(new BlogPostCategory { Category = category });
                        }
                    }
                }

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
                var post = await unitOfWork.PostBlogRepository.GetByIdAsync(id);
                if (post == null)
                {
                    return new GeneralResult
                    {
                        Success = false,
                        Errors = [new ResultError { Code = "PostNotFound", Message = "Blog post not found" }]
                    };
                }

                 unitOfWork.PostBlogRepository.Delete(post);



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
            Code = "UnexpectedError",
            Message = $"Unexpected error: {ex.Message}"
        }]
                };
            }
        }


    }
}
