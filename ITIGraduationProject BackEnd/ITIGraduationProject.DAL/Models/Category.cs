using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string? Name { get; set; } // e.g., "Vegetarian", "Desserts"

        public ICollection<RecipeCategory>? Recipes { get; set; } 
        public ICollection<BlogPostCategory> BlogPosts { get; set; } = new HashSet<BlogPostCategory>();
    }

}
