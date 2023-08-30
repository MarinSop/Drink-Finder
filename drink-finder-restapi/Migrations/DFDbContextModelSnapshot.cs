﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using drink_finder_restapi.Persistence.Contexts;

#nullable disable

namespace drink_finder_restapi.Migrations
{
    [DbContext(typeof(DFDbContext))]
    partial class DFDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("cities");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.Drink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<double>("Volume")
                        .HasColumnType("double");

                    b.Property<int>("drinkCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("drinkCategoryId");

                    b.ToTable("drinks");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.DrinkCategory", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("drinkCategories");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.Establishment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("UserId");

                    b.ToTable("establishments");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.EstablishmentDrink", b =>
                {
                    b.Property<int>("EstablishemntId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("DrinkId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<double>("price")
                        .HasColumnType("double");

                    b.HasKey("EstablishemntId", "DrinkId");

                    b.HasIndex("DrinkId");

                    b.ToTable("establishmentDrinks");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.Drink", b =>
                {
                    b.HasOne("drink_finder_restapi.Domain.Models.DrinkCategory", "drinkCategory")
                        .WithMany("drinks")
                        .HasForeignKey("drinkCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("drinkCategory");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.Establishment", b =>
                {
                    b.HasOne("drink_finder_restapi.Domain.Models.City", "City")
                        .WithMany("Establishments")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("drink_finder_restapi.Domain.Models.User", "User")
                        .WithMany("establishments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("User");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.EstablishmentDrink", b =>
                {
                    b.HasOne("drink_finder_restapi.Domain.Models.Drink", "drink")
                        .WithMany("establishemntDrinks")
                        .HasForeignKey("DrinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("drink_finder_restapi.Domain.Models.Establishment", "establishment")
                        .WithMany("establishemntDrinks")
                        .HasForeignKey("EstablishemntId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("drink");

                    b.Navigation("establishment");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.City", b =>
                {
                    b.Navigation("Establishments");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.Drink", b =>
                {
                    b.Navigation("establishemntDrinks");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.DrinkCategory", b =>
                {
                    b.Navigation("drinks");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.Establishment", b =>
                {
                    b.Navigation("establishemntDrinks");
                });

            modelBuilder.Entity("drink_finder_restapi.Domain.Models.User", b =>
                {
                    b.Navigation("establishments");
                });
#pragma warning restore 612, 618
        }
    }
}
