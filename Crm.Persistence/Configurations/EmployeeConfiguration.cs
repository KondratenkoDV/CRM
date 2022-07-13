using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Crm.Domain;

namespace Crm.Persistence.Configurations
{
    internal class EmployeeConfiguration: IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .HasIndex(e => new { e.Name })
                .IsUnique();

            builder
                .Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(e => e.LastName)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .HasOne(e => e.Position)
                .WithMany(p => p.Employees)
                .HasForeignKey(e => e.PositionId);
        }
    }
}
