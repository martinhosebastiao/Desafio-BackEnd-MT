using Microsoft.EntityFrameworkCore;
using MotoBusiness.Internal.Domain.Core.Entities.Rentals;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MotoBusiness.External.Infrastructure.Persistences.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("Rental");
            builder.HasKey(p => p.RentalId).HasName("RentalId");

            builder.Ignore(x => x.Delivery);
            builder.Ignore(x => x.Motorbike);

        }
    }
}

