namespace ITIGraduationProject.DAL
{
    public interface IPostBlogRepository : IGenericRepository<BlogPost>
    {
        Task<List<BlogPost>> GetByCategory(int catid);
    }
}