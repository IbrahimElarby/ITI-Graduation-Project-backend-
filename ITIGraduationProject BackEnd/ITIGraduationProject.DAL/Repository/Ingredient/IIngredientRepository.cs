using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public interface IIngredientRepository
    {
        public Task<Ingredient> GetByIdAsync(int id);

    }
}
