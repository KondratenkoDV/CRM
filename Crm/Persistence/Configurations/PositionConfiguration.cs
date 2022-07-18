using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Persistence.Configurations
{
    internal class PositionConfiguration: IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder
                .HasKey(p => p.Id);

            builder
                .HasIndex(p => new { p.Name })
                .IsUnique();

            builder
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
