namespace ITIGraduationProject.BL
{
    public interface IBlogPostManger
    {
        public Task<List<BlogPostDetailsDTO>> GetAll();

        public Task<BlogPostDetailsDTO> GetById(int id);

        public Task<BlogPostDetailsDTO> GetByCategory(int id);
        public Task<GeneralResult> UpdateAsync(BlogPostDetailsDTO item);

        public Task<GeneralResult> AddAsync(BlogPostDetailsDTO item);

        public Task<GeneralResult> DeleteAsync(int id);
    }
}