﻿// <auto-generated />
using System;
using ASUSport.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ASUSport.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ASUSport.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("SectionId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("TrainerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.HasIndex("TrainerId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ASUSport.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "client"
                        },
                        new
                        {
                            Id = 3,
                            Name = "trainer"
                        });
                });

            modelBuilder.Entity("ASUSport.Models.Section", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Duration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(60);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("SportObjectId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SportObjectId");

                    b.ToTable("Sections");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Duration = 0,
                            Name = "Свободное плавание",
                            SportObjectId = 1
                        },
                        new
                        {
                            Id = 2,
                            Duration = 0,
                            Name = "Плавание с тренером",
                            SportObjectId = 1
                        });
                });

            modelBuilder.Entity("ASUSport.Models.SportObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Capacity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.Property<string>("Location")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SportObjects");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Capacity = 64,
                            Name = "Бассейн"
                        },
                        new
                        {
                            Id = 2,
                            Capacity = 0,
                            Name = "Спортивная площадка"
                        },
                        new
                        {
                            Id = 3,
                            Capacity = 0,
                            Name = "Спортивный зал"
                        });
                });

            modelBuilder.Entity("ASUSport.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AccessCode")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Login = "trainer",
                            Password = "06ea17a8981d25b0315ff17bd1ebad3d95c15a3f8b764eaadb93612d9b61a54e",
                            RoleId = 3
                        },
                        new
                        {
                            Id = 2,
                            Login = "admin",
                            Password = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 3,
                            Login = "client1",
                            Password = "1917e33407c28366c8e3b975b17e7374589312676b90229adb4ce6e58552e223",
                            RoleId = 2
                        },
                        new
                        {
                            Id = 4,
                            Login = "client2",
                            Password = "3f455143e75d1e7fd659dea57023496da3bd9f2f8908d1e2ac32641cd819d3e3",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("ASUSport.Models.UserData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("LastName")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserData");
                });

            modelBuilder.Entity("EventUser", b =>
                {
                    b.Property<int>("ClientsId")
                        .HasColumnType("integer");

                    b.Property<int>("EventsId")
                        .HasColumnType("integer");

                    b.HasKey("ClientsId", "EventsId");

                    b.HasIndex("EventsId");

                    b.ToTable("EventsUsers");
                });

            modelBuilder.Entity("ASUSport.Models.Event", b =>
                {
                    b.HasOne("ASUSport.Models.Section", "Section")
                        .WithMany()
                        .HasForeignKey("SectionId");

                    b.HasOne("ASUSport.Models.User", "Trainer")
                        .WithMany()
                        .HasForeignKey("TrainerId");

                    b.Navigation("Section");

                    b.Navigation("Trainer");
                });

            modelBuilder.Entity("ASUSport.Models.Section", b =>
                {
                    b.HasOne("ASUSport.Models.SportObject", "SportObject")
                        .WithMany()
                        .HasForeignKey("SportObjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SportObject");
                });

            modelBuilder.Entity("ASUSport.Models.User", b =>
                {
                    b.HasOne("ASUSport.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ASUSport.Models.UserData", b =>
                {
                    b.HasOne("ASUSport.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EventUser", b =>
                {
                    b.HasOne("ASUSport.Models.User", null)
                        .WithMany()
                        .HasForeignKey("ClientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASUSport.Models.Event", null)
                        .WithMany()
                        .HasForeignKey("EventsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}