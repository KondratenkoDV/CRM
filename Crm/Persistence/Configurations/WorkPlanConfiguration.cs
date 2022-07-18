using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Persistence.Configurations
{
    internal class WorkPlanConfiguration: IEntityTypeConfiguration<WorkPlan>
    {
        public void Configure(EntityTypeBuilder<WorkPlan> builder)
        {
            builder
                .HasKey(w => w.Id);

            builder
                .HasIndex(w => new { w.DateStart, w.DateFinish })
                .IsUnique();
            builder
                .Property(w => w.DateStart)
                .IsRequired();

            builder
                .Property(w => w.DateFinish)
                .IsRequired();

            builder
                .HasOne(w => w.Contract)
                .WithMany(c => c.WorkPlans)
                .HasForeignKey(w => w.ContractId);
        }
    }
}
