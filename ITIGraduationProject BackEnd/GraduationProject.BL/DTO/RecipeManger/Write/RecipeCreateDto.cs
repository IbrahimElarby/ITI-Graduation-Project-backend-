using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.DTO.RecipeManger.Input
{
    public class RecipeCreateDto
    {
        public string Title { get; set; }
        public string Instructions { get; set; }
        public int PrepTime { get; set; }
        public string Description { get; set; }
        public int CookingTime { get; set; }
        public string CuisineType { get; set; }


        public AuthorNestedDTO Author { get; set; }

        //public int CreatedBy { get; set; }
        //public List<int> CategoryIds { get; set; }
        public List<string> CategoryNames { get; set; } = new();

        public List<RecipeIngredientCreateDto> Ingredients { get; set; }
    }
}
