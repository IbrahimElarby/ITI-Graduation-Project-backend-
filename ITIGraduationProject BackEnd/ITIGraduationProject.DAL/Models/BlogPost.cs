using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class BlogPost
    {
        public int BlogPostID { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; } // HTML/Markdown content
        public string? FeaturedImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        // Relationships
        public int AuthorID { get; set; }
        public ApplicationUser? Author { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<BlogPostCategory> Categories { get; set; } = new HashSet<BlogPostCategory>();
    }
}
