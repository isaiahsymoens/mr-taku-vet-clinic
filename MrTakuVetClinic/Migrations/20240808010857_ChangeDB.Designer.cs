﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MrTakuVetClinic.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MrTakuVetClinic.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240808010857_ChangeDB")]
    partial class ChangeDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("MrTakuVetClinic.Entities.Pet", b =>
                {
                    b.Property<int>("PetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Breed")
                        .HasColumnType("text");

                    b.Property<string>("PetName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int>("PetTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("PetId");

                    b.HasIndex("PetTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("MrTakuVetClinic.Entities.PetType", b =>
                {
                    b.Property<int>("PetTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("PetTypeId");

                    b.ToTable("PetTypes");

                    b.HasData(
                        new
                        {
                            PetTypeId = 1,
                            TypeName = "Dog"
                        },
                        new
                        {
                            PetTypeId = 2,
                            TypeName = "Cat"
                        });
                });

            modelBuilder.Entity("MrTakuVetClinic.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasAnnotation("EmailAddress", true);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("MiddleName")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int>("UserTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserTypeId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Active = true,
                            Email = "admin@gmail.com",
                            FirstName = "Admin",
                            LastName = "Admin",
                            Password = "admin",
                            UserTypeId = 1,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("MrTakuVetClinic.Entities.UserType", b =>
                {
                    b.Property<int>("UserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("UserTypeId");

                    b.ToTable("UserTypes");

                    b.HasData(
                        new
                        {
                            UserTypeId = 1,
                            TypeName = "Owner"
                        },
                        new
                        {
                            UserTypeId = 2,
                            TypeName = "Doctor"
                        },
                        new
                        {
                            UserTypeId = 3,
                            TypeName = "Pet Owner"
                        },
                        new
                        {
                            UserTypeId = 4,
                            TypeName = "Veterinary Assistant"
                        },
                        new
                        {
                            UserTypeId = 5,
                            TypeName = "Groomer"
                        });
                });

            modelBuilder.Entity("MrTakuVetClinic.Entities.Visit", b =>
                {
                    b.Property<int>("VisitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("character varying(100)")
                        .HasMaxLength(100);

                    b.Property<int>("PetId")
                        .HasColumnType("integer");

                    b.Property<int>("VisitTypeId")
                        .HasColumnType("integer");

                    b.HasKey("VisitId");

                    b.HasIndex("PetId");

                    b.HasIndex("VisitTypeId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("MrTakuVetClinic.Entities.VisitType", b =>
                {
                    b.Property<int>("VisitTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.HasKey("VisitTypeId");

                    b.ToTable("VisitsTypes");

                    b.HasData(
                        new
                        {
                            VisitTypeId = 1,
                            TypeName = "Consultation"
                        },
                        new
                        {
                            VisitTypeId = 2,
                            TypeName = "Dental care"
                        },
                        new
                        {
                            VisitTypeId = 3,
                            TypeName = "Grooming"
                        });
                });

            modelBuilder.Entity("MrTakuVetClinic.Entities.Pet", b =>
                {
                    b.HasOne("MrTakuVetClinic.Entities.PetType", "PetType")
                        .WithMany()
                        .HasForeignKey("PetTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MrTakuVetClinic.Entities.User", "User")
                        .WithMany("Pets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MrTakuVetClinic.Entities.User", b =>
                {
                    b.HasOne("MrTakuVetClinic.Entities.UserType", "UserType")
                        .WithMany()
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MrTakuVetClinic.Entities.Visit", b =>
                {
                    b.HasOne("MrTakuVetClinic.Entities.Pet", "Pet")
                        .WithMany("Visits")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MrTakuVetClinic.Entities.VisitType", "VisitType")
                        .WithMany()
                        .HasForeignKey("VisitTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
