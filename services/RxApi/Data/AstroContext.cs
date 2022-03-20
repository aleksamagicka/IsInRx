using Microsoft.EntityFrameworkCore;
using RxApi.Models;

namespace RxApi
{
    public partial class AstroContext : DbContext
    {
        public AstroContext()
        {
        }

        public AstroContext(DbContextOptions<AstroContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PlanetPosition> PlanetPositions { get; set; }

        public virtual DbSet<RetrogradePeriod> RetrogradePeriods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseNpgsql("Host=localhost;Database=astro;Username=postgres;Password=aleksa");
            //}

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlanetPosition>(entity =>
            {
                entity.ToTable("planet_positions7");

                entity.HasIndex(e => e.Time, "ix_planet_positions7_Time");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.Time).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<RetrogradePeriod>(entity =>
            {
                entity.ToTable("retrograde_periods");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('retrograde_periods_id_seq2'::regclass)");

                entity.Property(e => e.EndPositionId).HasColumnName("end_position_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("character varying");

                entity.Property(e => e.StartPositionId).HasColumnName("start_position_id");

                entity.HasOne(d => d.EndPosition)
                    .WithMany()
                    .HasForeignKey(d => d.EndPositionId)
                    .HasConstraintName("retrograde_periods_end_position_id_fkey");

                entity.HasOne(d => d.StartPosition)
                    .WithMany()
                    .HasForeignKey(d => d.StartPositionId)
                    .HasConstraintName("retrograde_periods_start_position_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
