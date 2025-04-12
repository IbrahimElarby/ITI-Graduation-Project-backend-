namespace ITIGraduationProject.DAL
{
    public interface IBlogPostCategoryRepository : IGenericRepository<BlogPostCategory>
    {
        public Task<bool> IsAssigned(int postid, int categoryid);
        public Task<BlogPostCategory?> GetByCompositeKeyAsync(int blogPostId, int categoryId);
    }
}