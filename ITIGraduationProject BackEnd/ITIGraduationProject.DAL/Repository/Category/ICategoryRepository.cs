namespace ITIGraduationProject.DAL
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public  Task<Category?> GetByName (string name);
    }
}