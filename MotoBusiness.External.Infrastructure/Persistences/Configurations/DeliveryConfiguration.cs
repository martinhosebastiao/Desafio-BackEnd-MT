using System;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace MotoBusiness.External.Infrastructure.Persistences.Configurations
{
	public class DeliveryConfiguration : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> builder)
        {
            builder.ToTable("Delivery");
            builder.HasKey(p => p.DeliveryId).HasName("DeliveryId");

            builder.HasIndex(x => x.CNPJ).IsUnique();
            builder.HasIndex(x => x.CNH.Number).IsUnique();

            builder.Ignore(x=> x.Rentals);
        }
    }
}

