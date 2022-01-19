using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OpenExchangeRates.Api.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BbsGbr> BbsGbrs { get; set; } = null!;
        public virtual DbSet<NbsEur> NbsEurs { get; set; } = null!;
        public virtual DbSet<OpenEur> OpenEurs { get; set; } = null!;
        public virtual DbSet<OpenGbr> OpenGbrs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP;Database=CurrencyDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BbsGbr>(entity =>
            {
                entity.ToTable("BbsGbr");

                entity.Property(e => e.Date).HasColumnType("date");
            });

            modelBuilder.Entity<NbsEur>(entity =>
            {
                entity.ToTable("NbsEur");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            modelBuilder.Entity<OpenEur>(entity =>
            {
                entity.ToTable("OpenEur");

                entity.Property(e => e.Date).HasColumnType("date");
            });

            modelBuilder.Entity<OpenGbr>(entity =>
            {
                entity.ToTable("OpenGbr");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Value).HasColumnName("value");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
