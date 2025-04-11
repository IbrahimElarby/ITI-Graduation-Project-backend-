using ITIGraduationProject.BL.DTO.RecipeManger.Read;

namespace ITIGraduationProject.BL
{
   public interface IIngredientManger
    {
        public Task<List<IngredientDto>> GetAll();

        public Task<IngredientDto> GetById(int id);

        public Task<GeneralResult> UpdateAsync(IngredientDto item);

        public Task<GeneralResult> AddAsync(IngredientDto item);

        public Task<GeneralResult> DeleteAsync(int id);
    }
}