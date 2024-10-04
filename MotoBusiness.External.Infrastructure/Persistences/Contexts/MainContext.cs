using Microsoft.EntityFrameworkCore;
using MotoBusiness.External.Infrastructure.Persistences.Configurations;
using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Entities.Rentals;

namespace MotoBusiness.External.Infrastructure.Persistences.Contexts
{
    public class MainContext: DbContext
	{
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public DbSet<Delivery> Deliveries { get; set; }
		public DbSet<Motorbike> Motorbikes { get; set; }
		public DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(
           DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableServiceProviderCaching();
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("mb");
            modelBuilder.UsePropertyAccessMode(PropertyAccessMode.Property);

            modelBuilder.ApplyConfiguration(new DeliveryConfiguration());
            modelBuilder.ApplyConfiguration(new RentalConfiguration());
            modelBuilder.ApplyConfiguration(new MotorbikeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

