using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class BlogPostCategory
    {
        public int BlogPostID { get; set; }
        public BlogPost? BlogPost { get; set; }
        public int CategoryID { get; set; }
        public Category? Category { get; set; }
    }
}
