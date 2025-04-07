using ITIGraduationProject.BL.DTO.BlogPostManger;
using ITIGraduationProject.DAL;
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

        public BlogPostManger(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }
        public async Task<List<BlogPostDetailsDTO>> GetAll()
        {
            var postsFromDb = await unitOfWork.PostBlogRepository.GetAll();

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
                    Email = p.Author?.Email ?? ""
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

        public Task<BlogPostDetailsDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BlogPostDetailsDTO> GetByCategory(int id)
        {
            throw new NotImplementedException();
        }
        public Task<GeneralResult> AddAsync(BlogPostDetailsDTO item)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResult> UpdateAsync(BlogPostDetailsDTO item)
        {
            throw new NotImplementedException();
        }
        public Task<GeneralResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


    }
}
