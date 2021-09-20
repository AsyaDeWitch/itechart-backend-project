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
    [Migration("20210917090631_Extended_User_Address_Allow_Null")]
    partial class Extended_User_Address_Allow_Null
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

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Platform")
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
                            DateCreated = new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Witcher 3: Wild Hunt",
                            Platform = 0,
                            TotalRating = 9.3000000000000007
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Witcher 3: Wild Hunt",
                            Platform = 7,
                            TotalRating = 9.1999999999999993
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTime(2015, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Witcher 3: Wild Hunt",
                            Platform = 4,
                            TotalRating = 9.0999999999999996
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTime(2019, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Witcher 3: Wild Hunt",
                            Platform = 9,
                            TotalRating = 8.5
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTime(2009, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Sims 3",
                            Platform = 0,
                            TotalRating = 8.5999999999999996
                        },
                        new
                        {
                            Id = 6,
                            DateCreated = new DateTime(2009, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Sims 3",
                            Platform = 1,
                            TotalRating = 8.5999999999999996
                        },
                        new
                        {
                            Id = 7,
                            DateCreated = new DateTime(1999, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Heroes of Might and Magic III",
                            Platform = 0,
                            TotalRating = 9.1999999999999993
                        },
                        new
                        {
                            Id = 8,
                            DateCreated = new DateTime(1999, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Heroes of Might and Magic III",
                            Platform = 1,
                            TotalRating = 9.1999999999999993
                        },
                        new
                        {
                            Id = 9,
                            DateCreated = new DateTime(1999, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Heroes of Might and Magic III",
                            Platform = 2,
                            TotalRating = 9.1999999999999993
                        },
                        new
                        {
                            Id = 10,
                            DateCreated = new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Grand Theft Auto V",
                            Platform = 6,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 11,
                            DateCreated = new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Grand Theft Auto V",
                            Platform = 3,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 12,
                            DateCreated = new DateTime(2014, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Grand Theft Auto V",
                            Platform = 7,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 13,
                            DateCreated = new DateTime(2014, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Grand Theft Auto V",
                            Platform = 4,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 14,
                            DateCreated = new DateTime(2015, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Grand Theft Auto V",
                            Platform = 0,
                            TotalRating = 9.5999999999999996
                        },
                        new
                        {
                            Id = 15,
                            DateCreated = new DateTime(2008, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "World of Warcraft: Wrath of the Lich King",
                            Platform = 0,
                            TotalRating = 9.0999999999999996
                        },
                        new
                        {
                            Id = 16,
                            DateCreated = new DateTime(2008, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "World of Warcraft: Wrath of the Lich King",
                            Platform = 1,
                            TotalRating = 9.0999999999999996
                        },
                        new
                        {
                            Id = 17,
                            DateCreated = new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Red Dead Redemption 2",
                            Platform = 7,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 18,
                            DateCreated = new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Red Dead Redemption 2",
                            Platform = 4,
                            TotalRating = 9.6999999999999993
                        },
                        new
                        {
                            Id = 19,
                            DateCreated = new DateTime(2018, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Red Dead Redemption 2",
                            Platform = 0,
                            TotalRating = 9.3000000000000007
                        },
                        new
                        {
                            Id = 20,
                            DateCreated = new DateTime(2011, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 0,
                            TotalRating = 9.4000000000000004
                        },
                        new
                        {
                            Id = 21,
                            DateCreated = new DateTime(2011, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 6,
                            TotalRating = 9.1999999999999993
                        },
                        new
                        {
                            Id = 22,
                            DateCreated = new DateTime(2011, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 3,
                            TotalRating = 9.5999999999999996
                        },
                        new
                        {
                            Id = 23,
                            DateCreated = new DateTime(2016, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 7,
                            TotalRating = 7.7000000000000002
                        },
                        new
                        {
                            Id = 24,
                            DateCreated = new DateTime(2016, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 4,
                            TotalRating = 8.1999999999999993
                        },
                        new
                        {
                            Id = 25,
                            DateCreated = new DateTime(2017, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "The Elder Scrolls V: Skyrim",
                            Platform = 9,
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
