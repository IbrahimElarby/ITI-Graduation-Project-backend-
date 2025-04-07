using ITIGraduationProject.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.BL
{
    public class BlogPostUpdateDTO
    {
        public int BlogPostID { get; set; }
    
        public string Content { get; set; } = string.Empty;
        public string FeaturedImageUrl { get; set; } = string.Empty;
        
        public List<string> CategoryNames { get; set; } = new();
    }
}
