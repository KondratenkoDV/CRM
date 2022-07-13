using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Crm.Domain;

namespace Crm.Persistence.Configurations
{
    internal class ClientConfiguration: IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .HasIndex(c => new { c.Name })
                .IsUnique();

            builder
                .Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(c => c.PhoneNumber)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(c => c.СodeOfTheCountry)
                .IsRequired();

            builder
                .Property(c => c.RegionCode)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .Property(c => c.SubscriberNumber)
                .HasMaxLength(255)
                .IsRequired();
        }
    }
}
