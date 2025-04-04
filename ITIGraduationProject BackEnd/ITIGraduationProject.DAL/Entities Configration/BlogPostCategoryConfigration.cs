using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL.Entities_Configration
{
    public class BlogPostCategoryConfigraton : IEntityTypeConfiguration<BlogPostCategory>
    {
        public void Configure(EntityTypeBuilder<BlogPostCategory> builder)
        {
            builder.HasKey(bpc => new { bpc.BlogPostID, bpc.CategoryID });
        }
    }
}
