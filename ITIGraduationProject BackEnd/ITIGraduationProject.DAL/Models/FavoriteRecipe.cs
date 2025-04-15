using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class FavoriteRecipe
    {
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }

        public int UserID { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
