using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class FavoriteRecipeCreateDto
    {
        public int RecipeID { get; set; }
        public int UserID { get; set; } // Optional if you derive it from the token
    }

}
