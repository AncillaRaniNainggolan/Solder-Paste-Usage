using Microsoft.EntityFrameworkCore;
using SolderPasteUsage.Models;

namespace SolderPasteUsage.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProductMaster> ProductMaster { get; set; }

        public DbSet<MaterialOrder> MaterialOrder { get; set; }

        public DbSet<WorkOrder> WorkOrder { get; set; }

        public DbSet<DemandProduction> DemandProduction { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DemandProduction>()
                .ToTable("DemandProduction")
                .HasKey(x => x.DemandId);

            modelBuilder.Entity<WorkOrder>()
                .ToTable("WorkOrder")
                .HasKey(x => x.WoId);

            modelBuilder.Entity<MaterialOrder>()
                .ToTable("MaterialOrder")
                .HasKey(x => x.OrderId);

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<ProductMaster>()
                .ToTable("ProductMaster");

            modelBuilder.Entity<StencilVolumePerCavity>()
                .ToTable("StencilVolumePerCavity");
        }
    }
}