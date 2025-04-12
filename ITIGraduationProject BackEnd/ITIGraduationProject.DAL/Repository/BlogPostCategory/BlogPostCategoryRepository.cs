using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class BlogPostCategoryRepository : GenericRepository<BlogPostCategory>, IBlogPostCategoryRepository
    {
        public BlogPostCategoryRepository(ApplicationDbContext cookingContext) : base(cookingContext)
        {
        }

        public async Task<bool> IsAssigned(int postid ,  int categoryid)
        {
            return await context.BlogPostCategories
                .AnyAsync(x => x.BlogPostID == postid && x.CategoryID == categoryid);
        }

        public async Task<BlogPostCategory?> GetByCompositeKeyAsync(int blogPostId, int categoryId)
        {
            return await context.Set<BlogPostCategory>()
                .FirstOrDefaultAsync(x => x.BlogPostID == blogPostId && x.CategoryID == categoryId);
        }
    }
}
