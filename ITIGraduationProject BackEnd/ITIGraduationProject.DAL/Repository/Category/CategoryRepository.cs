using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(ApplicationDbContext cookingContext) : base(cookingContext)
        {
        }

        public async Task<Category?> GetByName(string name)
        {
            return await context.Set<Category>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name);

        }
    }
}
