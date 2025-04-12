using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Seeders
{
    public class CommentSeeder : ISeeder
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasData(
                new Comment
                {
                    CommentID = 1,
                    RecipeID = 1,
                    Text = "Great recipe!",
                    CreatedAt = new DateTime(2025, 4, 1),
                    UserID = 1
                }
            );
        }
    }
}
