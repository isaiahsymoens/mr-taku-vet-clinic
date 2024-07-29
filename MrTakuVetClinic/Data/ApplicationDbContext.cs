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

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<UserType>()
                .HasKey(u => u.UserTypeId);

            modelBuilder.Entity<Pet>()
                .HasKey(p => p.PetId);

            modelBuilder.Entity<PetType>()
                .HasKey(p => p.PetTypeId);

            modelBuilder.Entity<VisitType>()
                .HasKey(v => v.VisitTypeId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Pets)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserType)
                .WithMany()
                .HasForeignKey(u => u.UserTypeId);

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
