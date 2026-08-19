using Microsoft.EntityFrameworkCore;
using SOS_API.Models;

namespace SOS_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("countries", "SOS");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Iso3Code)
                    .HasColumnName("iso3_code");

                entity.Property(e => e.Name)
                    .HasColumnName("name");

                entity.Property(e => e.Iso2Code)
                    .HasColumnName("iso2_code");
            });

            modelBuilder.Entity<Phone>(entity =>
            {
                entity.ToTable("phones", "SOS");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id");

                entity.Property(e => e.Category)
                    .HasColumnName("category");

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phone");

                entity.HasOne(e => e.Country)
                    .WithMany()
                    .HasForeignKey(e => e.CountryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}