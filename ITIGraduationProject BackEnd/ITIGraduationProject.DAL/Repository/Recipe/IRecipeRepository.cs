using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Repository
{
    public interface IRecipeRepository: IGenericRepository<Recipe>
    {
        Task<List<Recipe>> GetByCategory(int id);
        Task<List<Recipe>> GetByTitle(string title);
    }
}
