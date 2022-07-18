using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Persistence.Configurations
{
    internal class ContractConfiguration: IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasIndex(c => new { c.Subject, c.Address })
                .IsUnique();

            builder
                .Property(c => c.Subject)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(c => c.Address)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(c => c.Price)
                .IsRequired();

            builder
                .HasOne(c => c.Client)
                .WithMany(c => c.Contracts)
                .HasForeignKey(c => c.ClientId);

            builder
                .HasMany(c => c.Employees)
                .WithMany(e => e.Contracts)
                .UsingEntity(j => j.ToTable("EmployeesAndContracts"));
        }
    }
}
