using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(ApplicationDbContext cookingContext) : base(cookingContext)
        {
        }

        public override async Task<Ingredient?> GetByIdAsync(int id)
        {
            return await context.Set<Ingredient>()
                .FirstOrDefaultAsync(c => c.IngredientID == id);
        }
    }
}
