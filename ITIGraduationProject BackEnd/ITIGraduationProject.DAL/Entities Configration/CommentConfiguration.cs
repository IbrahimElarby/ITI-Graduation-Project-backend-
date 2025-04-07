using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ITIGraduationProject.DAL.Entities_Configration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(c => c.User)
               .WithMany() 
               .HasForeignKey(c => c.UserID);

            builder.HasOne(c => c.Recipe)
                .WithMany() 
                .HasForeignKey(c => c.RecipeID);
        }
    }
}
