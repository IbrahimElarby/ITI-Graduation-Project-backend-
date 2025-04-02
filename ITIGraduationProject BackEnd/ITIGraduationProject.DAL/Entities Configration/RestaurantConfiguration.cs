using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIGraduationProject.DAL
{
    public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            // Properties
            builder.Property(r => r.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(r => r.Location)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(r => r.CuisineType)
                .HasMaxLength(100);
        }
    }
}
