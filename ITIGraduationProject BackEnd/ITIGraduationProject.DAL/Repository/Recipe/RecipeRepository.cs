using Microsoft.EntityFrameworkCore;
namespace ITIGraduationProject.DAL.Repository
{
    public class RecipeRepository : GenericRepository<Recipe>, IRecipeRepository
    {
        private readonly ApplicationDbContext cookingContext;

        public RecipeRepository(ApplicationDbContext _cookingContext) : base(_cookingContext)
        {
            cookingContext = _cookingContext;
        }

        public async Task<List<Recipe>> GetByCategory(int catId)
        {
            return await cookingContext.Recipes
                .Include(r => r.Creator)
                .Include(r => r.RecipeIngredients)
                 .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .Include(r => r.Categories)
                    .ThenInclude(rc => rc.Category)
                .AsNoTracking()
                .AsSplitQuery()
                .Where(r => r.Categories.Any(rc => rc.CategoryID == catId))
                .ToListAsync();
        }

        public override async Task<List<Recipe>> GetAll()
        {
            return await cookingContext.Set<Recipe>()
                .Include(r => r.Creator)
                .Include(r => r.RecipeIngredients)
                .ThenInclude(ri=> ri.Ingredient)
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .Include(r => r.Categories)
                    .ThenInclude(rc => rc.Category)

                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();
        }


        public async override Task<Recipe?> GetByIdAsync(int id)
        {
            return await cookingContext.Set<Recipe>()
                 .Include(r => r.Creator)
                 .Include(r => r.RecipeIngredients)
                 .ThenInclude(ri => ri.Ingredient)
                 .Include(r => r.Ratings)
                 .Include(r => r.Comments)
                 
                 .Include(r => r.Categories)
                     .ThenInclude(rc => rc.Category)

              
                 .AsSplitQuery()
                 .FirstOrDefaultAsync(r=> r.RecipeID == id);
        }

        public  async Task<List<Recipe>> GetByTitle(string title)
        {
            return await cookingContext.Recipes
                .Include(r => r.Creator)
                .Include(r => r.RecipeIngredients)
                 .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.Ratings)
                .Include(r => r.Comments)
                .Include(r => r.Categories)
                    .ThenInclude(rc => rc.Category)
                .AsNoTracking()
                .Where(r => r.Title.Contains(title))
                .ToListAsync();
        }
    }
}
