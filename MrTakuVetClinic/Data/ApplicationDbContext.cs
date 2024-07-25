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
        public DbSet<Breed> Breeds { get; set; }

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

            modelBuilder.Entity<Breed>()
                .HasKey(b => b.BreedId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Pets)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserType)
                .WithMany()
                .HasForeignKey(u => u.UserTypeId);

            modelBuilder.Entity<Pet>()
                .HasOne(p => p.PetType)
                .WithMany()
                .HasForeignKey(p => p.PetTypeId);

            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Breed)
                .WithMany()
                .HasForeignKey(p => p.BreedId);
        }
    }
}
