namespace ITIGraduationProject.BL
{
    public interface IBlogPostManger
    {
        public Task<List<BlogPostDetailsDTO>> GetAll();

        public Task<BlogPostDetailsDTO> GetById(int id);

        public Task<List<BlogPostDetailsDTO>> GetByCategory(int id);
        public Task<GeneralResult> UpdateAsync(BlogPostUpdateDTO item);

        public Task<GeneralResult> AddAsync(BlogPostAddDTO item);

        public Task<GeneralResult> DeleteAsync(int id);
    }
}