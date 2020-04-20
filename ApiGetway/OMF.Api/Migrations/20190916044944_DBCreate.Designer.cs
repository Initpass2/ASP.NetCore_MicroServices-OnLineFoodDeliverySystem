﻿// <auto-generated />
using System;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OMF.Api.DTO.Repositories;

namespace OMF.Api.Migrations
{
    [DbContext(typeof(OMFStorageContext))]
    [Migration("20190916044944_DBCreate")]
    partial class DBCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OMF.Api.DTO.Models.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<int>("Budget");

                    b.Property<string>("Cuisine")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<IPoint>("Location")
                        .IsRequired();

                    b.Property<int>("MasterRestaurantID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Rating");

                    b.HasKey("Id");

                    b.ToTable("restaurants");
                });

            modelBuilder.Entity("OMF.Api.DTO.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MasterReviewId");

                    b.Property<string>("OrderId");

                    b.Property<decimal>("Rating");

                    b.Property<int>("RestaurantId");

                    b.Property<string>("ReviewText");

                    b.Property<DateTime>("UpdatedOn");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
