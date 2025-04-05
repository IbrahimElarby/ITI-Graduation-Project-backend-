
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        


       public IPostBlogRepository PostBlogRepository {  get; }

        public UnitOfWork(ApplicationDbContext cookingContext , IPostBlogRepository _PostBlogRepository)
        {
            context = cookingContext;
            PostBlogRepository = _PostBlogRepository;
            
        }


        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        
    }
}
