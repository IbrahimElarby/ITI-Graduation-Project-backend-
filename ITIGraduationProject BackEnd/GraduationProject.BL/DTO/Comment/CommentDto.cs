using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
     public class CommentDto
    {
        public string Content { get; set; }
        public int RecipeId { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserImg { get; set; }
    }
}
