

namespace ITIGraduationProject.DAL
{
    public interface IUnitOfWork
    {
       public IPostBlogRepository PostBlogRepository { get; }

        Task<int> SaveChangesAsync();
    }
}