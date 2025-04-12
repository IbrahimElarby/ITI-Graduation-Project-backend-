using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Seeders
{
    public class BlogPostCategorySeeder : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPostCategory>().HasData(
                new BlogPostCategory { BlogPostID = 1, CategoryID = 1 },
                new BlogPostCategory { BlogPostID = 1, CategoryID = 2 },
                new BlogPostCategory { BlogPostID = 2, CategoryID = 1 }
            );
        }
    }
}
