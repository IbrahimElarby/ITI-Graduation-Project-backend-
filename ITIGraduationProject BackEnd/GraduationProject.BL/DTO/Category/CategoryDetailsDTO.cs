using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class CategoryDetailsDTO
    {
        public int CategoryID { get; set; }
        public string? Name { get; set; }

        public List<BlogPostNestedDto> BlogPosts { get; set; } = new ();
        public List<RecipeNestedDTO> Recipes { get; set; } = new ();

    }
}
