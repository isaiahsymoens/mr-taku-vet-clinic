﻿using Microsoft.EntityFrameworkCore;
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

            modelBuilder.Entity<UserType>().HasData(
                new UserType { UserTypeId = 1, TypeName = "Owner" },
                new UserType { UserTypeId = 2, TypeName = "Doctor" },
                new UserType { UserTypeId = 3, TypeName = "Pet Owner" },
                new UserType { UserTypeId = 4, TypeName = "Veterinary Assistant" },
                new UserType { UserTypeId = 5, TypeName = "Groomer" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@gmail.com",
                    Username = "admin",
                    Password = "admin",
                    UserTypeId = 1,
                    Active = true,
                }
            );

            modelBuilder.Entity<PetType>().HasData(
                new PetType { PetTypeId = 1, TypeName = "Dog" },
                new PetType { PetTypeId = 2, TypeName = "Cat" }
            );

            modelBuilder.Entity<VisitType>().HasData(
                new VisitType { VisitTypeId = 1, TypeName = "Consultation" },
                new VisitType { VisitTypeId = 2, TypeName = "Dental care" },
                new VisitType { VisitTypeId = 3, TypeName = "Grooming" }
            );

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

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasKey(u => u.UserTypeId);

                entity.Property(u => u.TypeName)
                    .IsRequired()
                    .HasMaxLength(50);

            });

            modelBuilder.Entity<Pet>(entity =>
            { 
                entity.HasKey(p => p.PetId);

                entity.Property(p => p.UserId)
                    .IsRequired();

                entity.Property(p => p.PetName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.PetTypeId)
                    .IsRequired();

                entity.HasMany(p => p.Visits)
                    .WithOne(p => p.Pet)
                    .HasForeignKey(p => p.PetId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.PetType)
                    .WithMany()
                    .HasForeignKey(p => p.PetTypeId);
            });

            modelBuilder.Entity<PetType>(entity =>
            {
                entity.HasKey(p => p.PetTypeId);

                entity.Property(p => p.TypeName)
                    .IsRequired()
                    .HasMaxLength(50);

            });

            modelBuilder.Entity<Visit>(entity =>
            { 
                entity.HasKey(v => v.VisitId);

                entity.Property(v => v.VisitTypeId)
                    .IsRequired();

                entity.Property(v => v.Date)
                    .IsRequired();

                entity.Property(v => v.PetId)
                    .IsRequired();

                entity.Property(v => v.Notes)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(v => v.VisitType)
                    .WithMany()
                    .HasForeignKey(v => v.VisitTypeId);
            });

            modelBuilder.Entity<VisitType>(entity =>
            {
                entity.HasKey(v => v.VisitTypeId);

                entity.Property(v => v.TypeName)
                    .IsRequired()
                    .HasMaxLength(50);

            });
        }
    }
}
