using Microsoft.EntityFrameworkCore;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MotoBusiness.External.Infrastructure.Persistences.Configurations
{
    public class MotorbikeConfiguration : IEntityTypeConfiguration<Motorbike>
    {
        public void Configure(EntityTypeBuilder<Motorbike> builder)
        {
            builder.ToTable("Motorbike");
            builder.HasKey(p => p.MotorbikeId).HasName("MotorbikeId");

            builder.HasIndex(x => x.Plate).IsUnique();

            builder.Ignore(x => x.Rentals);

        }
    }
}

