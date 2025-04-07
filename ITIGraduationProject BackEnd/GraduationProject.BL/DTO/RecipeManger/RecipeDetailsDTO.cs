using ITIGraduationProject.BL;
using ITIGraduationProject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.DTO.RecipeManger
{
    public class RecipeDetailsDTO
    {
        public int RecipeID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public int PrepTime { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CookingTime { get; set; }
        public string CuisineType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }


        public AuthorNestedDTO Author { get; set; } = new();
        public List<CommentNestedDTO> Comments { get; set; } = new();
        public List<string> CategoryNames { get; set; } = new();
        public List<RecipeIngredientDTO> RecipeIngredients { get; set; } = new();
        public List<RatingDTO> Ratings { get; set; } = new();

    }
}
