using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess.Context
{
    public partial class AspenCapitalContext : DbContext
    {
        public AspenCapitalContext()
        {
        }

        public AspenCapitalContext(DbContextOptions<AspenCapitalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actual> Actuals { get; set; }
        public virtual DbSet<Projection> Projections { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {  
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Actual>(entity =>
            {
                entity.Property(e => e.ActualId).HasColumnName("ActualID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.ProjectionId).HasColumnName("ProjectionID");

                //entity.HasOne(d => d.Projection)
                //    .WithMany(p => p.Actuals)
                //    .HasForeignKey(d => d.ProjectionId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__Actuals__Project__1273C1CD");
            });

            modelBuilder.Entity<Projection>(entity =>
            {
                entity.Property(e => e.ProjectionId).HasColumnName("ProjectionID");

                entity.Property(e => e.Xml).HasColumnName("XML");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
