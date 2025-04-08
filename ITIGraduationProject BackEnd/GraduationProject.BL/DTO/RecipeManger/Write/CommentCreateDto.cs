using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL.DTO.RecipeManger.Write
{
    public class CommentCreateDto
    {
        public string Text { get; set; }
        public int? RecipeID { get; set; }
        public int? BlogPostID { get; set; }
        public int UserID { get; set; }
    }
}
