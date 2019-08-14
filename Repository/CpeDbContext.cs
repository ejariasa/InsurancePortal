using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Repository.Entities
{
    public partial class CpeDbContext : DbContext
    {
        public CpeDbContext()
        {
        }

        public CpeDbContext(DbContextOptions<CpeDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomerPolicy> CustomerPolicy { get; set; }
        public virtual DbSet<Policy> Policy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=YUXARM0018L;Database=Insurance;Integrated Security=False;User ID=earias;Password=Eja.30230267;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.IdentifyNumber)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerPolicy>(entity =>
            {
                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CustomerPolicy)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPolicy_CustomerPolicy");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.CustomerPolicy)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CustomerPolicy_CustomerPolicy1");
            });

            modelBuilder.Entity<Policy>(entity =>
            {
                entity.Property(e => e.CoveragePecentage).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.CoverageType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RiskType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });
        }
    }
}
