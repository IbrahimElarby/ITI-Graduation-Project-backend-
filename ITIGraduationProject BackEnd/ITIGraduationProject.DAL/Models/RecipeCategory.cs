using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class RecipeCategory
    {
        public int RecipeID { get; set; }
        public Recipe? Recipe { get; set; }
        public int CategoryID { get; set; }
        public Category? Category { get; set; }
    }
}
