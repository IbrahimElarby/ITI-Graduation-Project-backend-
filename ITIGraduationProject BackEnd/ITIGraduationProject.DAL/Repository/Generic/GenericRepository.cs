using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext cookingContext)
        {
            context = cookingContext;
        }
        public virtual async Task<List<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {

        }

        public  void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }


     
    }
}
