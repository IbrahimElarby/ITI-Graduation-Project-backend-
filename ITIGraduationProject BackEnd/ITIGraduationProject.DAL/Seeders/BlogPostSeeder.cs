using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Seeders
{
    public class BlogPostSeeder : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    BlogPostID = 1,
                    Title = "10 Tips for Cooking Perfect Pasta",
                    Content = "Start with boiling water, salt it properly, and don't forget to stir occasionally.",
                    CreatedAt = new DateTime(2025, 4, 1),
                    AuthorID = 1
                },
                new BlogPost
                {
                    BlogPostID = 2,
                    Title = "Benefits of Mediterranean Diet",
                    Content = "Discover how olive oil, fresh veggies, and lean protein contribute to long-term health.",
                    CreatedAt = new DateTime(2025, 4, 1),
                    AuthorID = 2
                },
                new BlogPost
                {
                    BlogPostID = 3,
                    Title = "Why Breakfast Matters",
                    Content = "Explore the science behind the most important meal of the day.",
                    CreatedAt = new DateTime(2025, 4, 1),
                    AuthorID = 3
                },
            new BlogPost
            {
                BlogPostID = 4,
                Title = "Healthy Eating",
                Content = "Eating healthy is important for maintaining a balanced diet.",
                CreatedAt = new DateTime(2025, 4, 1),
                AuthorID = 1
            },
                new BlogPost
                {
                    BlogPostID = 5,
                    Title = "Cooking Tips",
                    Content = "Here are some tips for cooking delicious meals.",
                    CreatedAt = new DateTime(2025, 4, 1),
                    AuthorID = 2
                }
            );
        }
    }
}
