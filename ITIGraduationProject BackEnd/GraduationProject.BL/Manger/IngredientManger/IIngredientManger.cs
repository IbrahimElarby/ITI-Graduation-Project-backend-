using ITIGraduationProject.BL.DTO.RecipeManger.Read;

namespace ITIGraduationProject.BL
{
   public interface IIngredientManger
    {
        public Task<List<RecipeIngredientDTO>> GetAll();

        public Task<RecipeIngredientDTO> GetById(int id);

        public Task<GeneralResult> UpdateAsync(RecipeIngredientDTO item);

        public Task<GeneralResult> AddAsync(RecipeIngredientDTO item);

        public Task<GeneralResult> DeleteAsync(int id);
    }
}