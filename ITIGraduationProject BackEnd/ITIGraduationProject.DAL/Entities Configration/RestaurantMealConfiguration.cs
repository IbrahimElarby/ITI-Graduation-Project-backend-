using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class RestaurantMealConfiguration : IEntityTypeConfiguration<RestaurantMeal>
    {
        public void Configure(EntityTypeBuilder<RestaurantMeal> builder)
        {
            builder.HasKey(m => m.MealID);
            builder.Property(m => m.Name)
                .HasMaxLength(255);
            builder.Property(m => m.Price)
                .HasColumnType("decimal(18, 2)");
            builder.Property(m => m.Calories)
                .HasColumnType("int");
            builder.HasOne(m => m.Restaurant)
                .WithMany(r => r.RestaurantMeals)
                .HasForeignKey(m => m.RestaurantID);
        }
    }
}
