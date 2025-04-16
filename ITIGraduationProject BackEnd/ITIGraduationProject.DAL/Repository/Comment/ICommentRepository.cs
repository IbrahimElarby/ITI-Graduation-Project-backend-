namespace ITIGraduationProject.DAL
{
    public interface ICommentRepository
    {
       public Task AddAsync(Comment comment);
       public Task<List<Comment>> GetByRecipeIdAsync(int recipeId);
       public Task<Comment?> GetByIdAsync(int commentId);
       public Task UpdateAsync(Comment comment);
       public Task DeleteAsync(Comment comment);
    }
}