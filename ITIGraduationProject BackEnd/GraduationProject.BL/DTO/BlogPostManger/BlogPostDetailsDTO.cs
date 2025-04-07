using ITIGraduationProject.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class BlogPostDetailsDTO
    {
        public int BlogPostID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string FeaturedImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public AuthorNestedDTO Author { get; set; } = new(); 
        public List<CommentNestedDTO> Comments { get; set; } = new();
        public List<string> CategoryNames { get; set; } = new();
    }
}
