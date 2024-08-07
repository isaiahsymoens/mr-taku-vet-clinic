using Microsoft.EntityFrameworkCore;
using MrTakuVetClinic.Entities;

namespace MrTakuVetClinic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<PetType> PetTypes { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VisitType> VisitsTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);

                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.MiddleName)
                    .HasMaxLength(50);

                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasAnnotation("EmailAddress", true);

                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasIndex(u => u.Username)
                    .IsUnique();

                entity.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.UserTypeId)
                    .IsRequired();

                entity.HasMany(u => u.Pets)
                    .WithOne(u => u.User)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.UserType)
                    .WithMany()
                    .HasForeignKey(u => u.UserTypeId);
            });

            modelBuilder.Entity<UserType>()
                .HasKey(u => u.UserTypeId);

            modelBuilder.Entity<Pet>()
                .HasKey(p => p.PetId);

            modelBuilder.Entity<PetType>()
                .HasKey(p => p.PetTypeId);

            modelBuilder.Entity<VisitType>()
                .HasKey(v => v.VisitTypeId);

            modelBuilder.Entity<Pet>()
                .HasMany(p => p.Visits)
                .WithOne(p => p.Pet)
                .HasForeignKey(p => p.PetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Pet>()
                .HasOne(p => p.PetType)
                .WithMany()
                .HasForeignKey(p => p.PetTypeId);

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.VisitType)
                .WithMany()
                .HasForeignKey(v => v.VisitTypeId);
        }
    }
}
