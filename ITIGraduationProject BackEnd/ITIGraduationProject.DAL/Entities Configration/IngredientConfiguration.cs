using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            // Properties
            builder.Property(i => i.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(i => i.CaloriesPer100g)
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.Protein)
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.Carbs)
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.Fats)
                .HasColumnType("decimal(18,2)");
        }
    }
}
