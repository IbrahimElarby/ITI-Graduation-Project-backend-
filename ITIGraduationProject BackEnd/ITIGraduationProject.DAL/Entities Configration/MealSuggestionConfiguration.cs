using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class MealSuggestionConfiguration : IEntityTypeConfiguration<MealSuggestion>
    {
        public void Configure(EntityTypeBuilder<MealSuggestion> builder)
        {
            builder.HasKey(ms=>ms.SuggestionID);
            builder.Property(ms => ms.Budget)
                .HasColumnType("decimal(18, 2)");
            builder.HasOne(ms => ms.User)
                .WithMany(u => u.MealSuggestions)
                .HasForeignKey(ms => ms.UserID);
            builder.HasOne(ms => ms.Restaurant)
                .WithMany(r => r.MealSuggestions)
                .HasForeignKey(ms => ms.RestaurantID);
        }
    }

}
