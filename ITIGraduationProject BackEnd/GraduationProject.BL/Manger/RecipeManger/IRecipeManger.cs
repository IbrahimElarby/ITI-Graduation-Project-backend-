using ITIGraduationProject.BL.DTO.RecipeManger.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITIGraduationProject.BL.DTO.RecipeManger.Output;

namespace ITIGraduationProject.BL.Manger
{
    public interface IRecipeManger
    {
        public Task<List<RecipeDetailsDTO>> GetAll();
        public Task<RecipeDetailsDTO> GetById(int id);
        public Task<List<RecipeDetailsDTO>> GetByTitle(string title);
        public Task<List<RecipeDetailsDTO>> GetByCategory(int id);
        public Task<GeneralResult> UpdateAsync(RecipeDetailsDTO item);
        public Task<GeneralResult> AddAsync(RecipeCreateDto item);
        public Task<GeneralResult> DeleteAsync(int id);

    }
}
