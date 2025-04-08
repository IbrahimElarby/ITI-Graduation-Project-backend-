using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Repository.Ingredient
{
    public class IngredientRepository : GenericRepository<DAL.Ingredient>, IIngredientRepository
    {
        public IngredientRepository(ApplicationDbContext cookingContext) : base(cookingContext)
        {
        }

        public async Task<DAL.Ingredient> GetByIdAsync(int id)
        {
            return await context.Set<DAL.Ingredient>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IngredientID == id);
        }
    }
}
