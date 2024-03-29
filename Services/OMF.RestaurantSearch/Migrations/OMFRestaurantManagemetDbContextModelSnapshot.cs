﻿// <auto-generated />
using System;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OMF.RestaurantManagement.Domain.Repositories;

namespace OMF.RestaurantManagement.Migrations
{
    [DbContext(typeof(OMFRestaurantManagemetDbContext))]
    partial class OMFRestaurantManagemetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OMF.RestaurantManagement.Domain.Models.RestaurantMenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MenuID");

                    b.Property<int>("ResturantID");

                    b.HasKey("Id");

                    b.HasIndex("ResturantID");

                    b.HasIndex("MenuID", "ResturantID")
                        .IsUnique();

                    b.ToTable("restaurantMenuItems");
                });

            modelBuilder.Entity("OMF.RestaurantSearch.Domain.Models.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Desctiption");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.ToTable("budgets");
                });

            modelBuilder.Entity("OMF.RestaurantSearch.Domain.Models.Cuisine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("cuisines");
                });

            modelBuilder.Entity("OMF.RestaurantSearch.Domain.Models.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Ingredients")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("ItemName")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.ToTable("menus");
                });

            modelBuilder.Entity("OMF.RestaurantSearch.Domain.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<int>("BudgetId");

                    b.Property<string>("City");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("CuisineId");

                    b.Property<bool>("IsActive");

                    b.Property<IPoint>("Location")
                        .IsRequired()
                        .HasColumnType("geometry");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<string>("Zip")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.HasIndex("CuisineId");

                    b.ToTable("restaurants");
                });

            modelBuilder.Entity("OMF.RestaurantManagement.Domain.Models.RestaurantMenuItem", b =>
                {
                    b.HasOne("OMF.RestaurantSearch.Domain.Models.Menu", "Budget")
                        .WithMany("restaurantMenuItems")
                        .HasForeignKey("MenuID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OMF.RestaurantSearch.Domain.Models.Restaurant", "Restaurant")
                        .WithMany("restaurantMenuItems")
                        .HasForeignKey("ResturantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OMF.RestaurantSearch.Domain.Models.Restaurant", b =>
                {
                    b.HasOne("OMF.RestaurantSearch.Domain.Models.Budget", "Budget")
                        .WithMany("Restaurant")
                        .HasForeignKey("BudgetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OMF.RestaurantSearch.Domain.Models.Cuisine", "Cuisine")
                        .WithMany("restaurants")
                        .HasForeignKey("CuisineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
