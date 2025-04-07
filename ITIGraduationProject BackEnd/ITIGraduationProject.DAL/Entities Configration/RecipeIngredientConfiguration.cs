using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class RecipeIngredientConfiguration : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            // Composite Key
            builder.HasKey(ri => new { ri.RecipeID, ri.IngredientID });

            // Relationships
            builder.HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientID)
                .OnDelete(DeleteBehavior.Restrict);

            // Properties
            builder.Property(ri => ri.Quantity)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(ri => ri.Unit)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
