using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string? Title { get; set; }
        public string?   Instructions { get; set; }

        public int PrepTime { get; set; } 
        public string? Description { get; set; }
        public int CookingTime { get; set; }
        public string? CuisineType { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relationships
        public int CreatedBy { get; set; }
        public ApplicationUser? Creator { get; set; }
        public ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<Comment>? Comments { get; set; } 
        public ICollection<RecipeCategory>? Categories { get; set; } 
    }
}
