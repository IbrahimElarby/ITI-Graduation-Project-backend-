using ITIGraduationProject.BL.DTO.Category;

namespace ITIGraduationProject.BL.Manger.CategoryManger
{
    public interface ICategoryManger 
    {
        public Task<List<CategoryDetailsDTO>> GetAll();

        public Task<CategoryDetailsDTO> GetById(int id);

        public Task<CategoryDetailsDTO> GetByName(string name);
        public Task<GeneralResult> UpdateAsync(CategoryAddDTO item);

        public Task<GeneralResult> AddAsync(CategoryAddDTO item);

        public Task<GeneralResult> DeleteAsync(int id);
    }
}