using ITIGraduationProject.BL.DTO.RecipeManger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.Manger
{
    public interface IRecipeManger
    {
        public Task<List<RecipeDetailsDTO>> GetAll();
        public Task<RecipeDetailsDTO> GetById(int id);
        public Task<RecipeDetailsDTO> GetByTitle(string title);
        public Task<List<RecipeDetailsDTO>> GetByCategory(int id);
        public Task<GeneralResult> UpdateAsync(RecipeDetailsDTO item);
        public Task<GeneralResult> AddAsync(RecipeDetailsDTO item);
        public Task<GeneralResult> DeleteAsync(int id);

    }
}
