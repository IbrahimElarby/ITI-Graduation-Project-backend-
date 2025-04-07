using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class Comment
    {
        public int CommentID { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }

        // Link to Recipe or BlogPost
        public int? RecipeID { get; set; }
        public Recipe? Recipe { get; set; }
        public int? BlogPostID { get; set; }
        public BlogPost? BlogPost { get; set; }

        // User who wrote the comment
        public int UserID { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
