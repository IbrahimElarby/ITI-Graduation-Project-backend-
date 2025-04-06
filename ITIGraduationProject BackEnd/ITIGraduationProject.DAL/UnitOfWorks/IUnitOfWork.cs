

using ITIGraduationProject.DAL.Repository;

namespace ITIGraduationProject.DAL
{
    public interface IUnitOfWork
    {
       public IPostBlogRepository PostBlogRepository { get; }
       public IRecipeRepository RecipeRepository { get; }

        Task<int> SaveChangesAsync();
    }
}