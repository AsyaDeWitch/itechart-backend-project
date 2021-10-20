﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210920124323_Product_Extension")]
    partial class Product_Extension
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RIL.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("EntranceNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("FlatNumber")
                        .HasColumnType("int");

                    b.Property<int>("FloorNumber")
                        .HasColumnType("int");

                    b.Property<string>("HouseBuilding")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasDefaultValue("-");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("RIL.Models.ExtendedUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("AddressDeliveryId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("AddressDeliveryId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("RIL.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Background")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("https://firebasestorage.googleapis.com/v0/b/lab-web-app-299ef.appspot.com/o/empty_background.jpg?alt=media&token=0ad4ed61-2a4e-4b48-be4b-d683282d5fc5e");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Logo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("https://firebasestorage.googleapis.com/v0/b/lab-web-app-299ef.appspot.com/o/empty_image.png?alt=media&token=fdbc866c-69d0-458e-a162-29a17eca00fe");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Platform")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<double>("TotalRating")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DateCreated");

                    b.HasIndex("Name");

                    b.HasIndex("Platform");

                    b.HasIndex("TotalRating");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Count = 10,
                            DateCreated = new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "The Witcher 3: Wild Hunt",
                            Platform = 0,
                            Price = 19.989999999999998,
                            Rating = 3,
                            TotalRating = 9.3000000000000007
                        },
                        new
                        {
                            Id = 2,
                            Count = 5,
                            DateCreated = new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "The Witcher 3: Wild Hunt",
                            Platform = 7,
                            Price = 19.989999999999998,
                            Rating = 3,
                            TotalRating = 9.1999999999999993
                        },
                        new
                        {
                            Id = 3,
                            Count = 7,
                            DateCreated = new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "The Witcher 3: Wild Hunt",
                            Platform = 4,
                            Price = 19.989999999999998,
                            Rating = 3,
                            TotalRating = 9.0999999999999996
                        },
                        new
                        {
                            Id = 4,
                            Count = 8,
                            DateCreated = new DateTime(2019, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "The Witcher 3: Wild Hunt",
                            Platform = 9,
                            Price = 19.989999999999998,
                            Rating = 3,
                            TotalRating = 8.5
                        },
                        new
                        {
                            Id = 5,
                            Count = 8,
                            DateCreated = new DateTime(2009, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Simulation",
                            Name = "The Sims 3",
                            Platform = 0,
                            Price = 19.989999999999998,
                            Rating = 1,
                            TotalRating = 8.5999999999999996
                        },
                        new
                        {
                            Id = 6,
                            Count = 13,
                            DateCreated = new DateTime(2009, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Simulation",
                            Name = "The Sims 3",
                            Platform = 1,
                            Price = 19.989999999999998,
                            Rating = 1,
                            TotalRating = 8.5999999999999996
                        },
                        new
                        {
                            Id = 7,
                            Count = 6,
                            DateCreated = new DateTime(1999, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Turn-based strategy with RPG elements",
                            Name = "Heroes of Might and Magic III",
                            Platform = 0,
                            Price = 8.25,
                            Rating = 0,
                            TotalRating = 9.1999999999999993
                        },
                        new
                        {
                            Id = 8,
                            Count = 3,
                            DateCreated = new DateTime(1999, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Turn-based strategy with RPG elements",
                            Name = "Heroes of Might and Magic III",
                            Platform = 1,
                            Price = 8.25,
                            Rating = 0,
                            TotalRating = 9.1999999999999993
                        },
                        new
                        {
                            Id = 9,
                            Count = 1,
                            DateCreated = new DateTime(1999, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Turn-based strategy with RPG elements",
                            Name = "Heroes of Might and Magic III",
                            Platform = 2,
                            Price = 8.25,
                            Rating = 0,
                            TotalRating = 9.1999999999999993
                        },
                        new
                        {
                            Id = 10,
                            Count = 6,
                            DateCreated = new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action-adventure",
                            Name = "Grand Theft Auto V",
                            Platform = 6,
                            Price = 12.58,
                            Rating = 3,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 11,
                            Count = 8,
                            DateCreated = new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action-adventure",
                            Name = "Grand Theft Auto V",
                            Platform = 3,
                            Price = 12.58,
                            Rating = 3,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 12,
                            Count = 12,
                            DateCreated = new DateTime(2014, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action-adventure",
                            Name = "Grand Theft Auto V",
                            Platform = 7,
                            Price = 12.58,
                            Rating = 3,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 13,
                            Count = 4,
                            DateCreated = new DateTime(2014, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action-adventure",
                            Name = "Grand Theft Auto V",
                            Platform = 4,
                            Price = 12.58,
                            Rating = 3,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 14,
                            Count = 2,
                            DateCreated = new DateTime(2015, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action-adventure",
                            Name = "Grand Theft Auto V",
                            Platform = 0,
                            Price = 12.58,
                            Rating = 3,
                            TotalRating = 9.5999999999999996
                        },
                        new
                        {
                            Id = 15,
                            Count = 2,
                            DateCreated = new DateTime(2008, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "World of Warcraft: Wrath of the Lich King",
                            Platform = 0,
                            Price = 40.450000000000003,
                            Rating = 1,
                            TotalRating = 9.0999999999999996
                        },
                        new
                        {
                            Id = 16,
                            Count = 3,
                            DateCreated = new DateTime(2008, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "World of Warcraft: Wrath of the Lich King",
                            Platform = 1,
                            Price = 40.450000000000003,
                            Rating = 1,
                            TotalRating = 9.0999999999999996
                        },
                        new
                        {
                            Id = 17,
                            Count = 25,
                            DateCreated = new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action-adventure",
                            Name = "Red Dead Redemption 2",
                            Platform = 7,
                            Price = 34.990000000000002,
                            Rating = 3,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 18,
                            Count = 2,
                            DateCreated = new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action-adventure",
                            Name = "Red Dead Redemption 2",
                            Platform = 4,
                            Price = 34.990000000000002,
                            Rating = 3,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 19,
                            Count = 14,
                            DateCreated = new DateTime(2018, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "Action-adventure",
                            Name = "Red Dead Redemption 2",
                            Platform = 0,
                            Price = 34.990000000000002,
                            Rating = 3,
                            TotalRating = 9.3000000000000007
                        },
                        new
                        {
                            Id = 20,
                            Count = 3,
                            DateCreated = new DateTime(2011, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 0,
                            Price = 7.9900000000000002,
                            Rating = 3,
                            TotalRating = 9.4000000000000004
                        },
                        new
                        {
                            Id = 21,
                            Count = 7,
                            DateCreated = new DateTime(2011, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 6,
                            Price = 7.9900000000000002,
                            Rating = 3,
                            TotalRating = 9.1999999999999993
                        },
                        new
                        {
                            Id = 22,
                            Count = 5,
                            DateCreated = new DateTime(2011, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 3,
                            Price = 7.9900000000000002,
                            Rating = 3,
                            TotalRating = 9.5999999999999996
                        },
                        new
                        {
                            Id = 23,
                            Count = 3,
                            DateCreated = new DateTime(2016, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 7,
                            Price = 7.9900000000000002,
                            Rating = 3,
                            TotalRating = 7.7000000000000002
                        },
                        new
                        {
                            Id = 24,
                            Count = 5,
                            DateCreated = new DateTime(2016, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 4,
                            Price = 7.9900000000000002,
                            Rating = 3,
                            TotalRating = 8.1999999999999993
                        },
                        new
                        {
                            Id = 25,
                            Count = 4,
                            DateCreated = new DateTime(2017, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Genre = "RPG",
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 9,
                            Price = 7.9900000000000002,
                            Rating = 3,
                            TotalRating = 8.4000000000000004
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("RIL.Models.ExtendedUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("RIL.Models.ExtendedUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RIL.Models.ExtendedUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("RIL.Models.ExtendedUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RIL.Models.ExtendedUser", b =>
                {
                    b.HasOne("RIL.Models.Address", "AddressDelivery")
                        .WithMany()
                        .HasForeignKey("AddressDeliveryId");

                    b.Navigation("AddressDelivery");
                });
#pragma warning restore 612, 618
        }
    }
}
