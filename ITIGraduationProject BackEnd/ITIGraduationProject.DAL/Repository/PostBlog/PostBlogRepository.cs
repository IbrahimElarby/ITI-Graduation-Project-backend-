using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class PostBlogRepository : GenericRepository<BlogPost>, IPostBlogRepository
    {
        private readonly ApplicationDbContext cookingContext;

        public PostBlogRepository(ApplicationDbContext _cookingContext) : base(_cookingContext)
        {
            cookingContext = _cookingContext;
        }

        public override async Task<List<BlogPost>> GetAll()
        {
            return await cookingContext.Set<BlogPost>()
                .Include(bp => bp.Author)
                .Include(bp => bp.Comments)
                .Include(bp => bp.Categories)
                    .ThenInclude(bpc => bpc.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetByCategory(int catid)
        {
            return await cookingContext.Set<BlogPost>()
                .Include(bp => bp.Categories)
                .ThenInclude(bpc => bpc.Category)
                .AsNoTracking()
                .Where(bp => bp.Categories.Any(bpc => bpc.CategoryID == catid))
                .ToListAsync();
        }

    }
}
