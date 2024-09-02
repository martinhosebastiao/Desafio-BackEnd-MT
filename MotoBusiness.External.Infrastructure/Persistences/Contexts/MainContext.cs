using System;
using Microsoft.EntityFrameworkCore;
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
            this.ChangeTracker.LazyLoadingEnabled = false;
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
			modelBuilder.Entity<Motorbike>().HasIndex(x=> x.Plate).IsUnique();
			modelBuilder.Entity<Delivery>().HasIndex(x=> x.CNPJ).IsUnique();
			modelBuilder.Entity<Delivery>().HasIndex(x=> x.CNH.Number).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}

