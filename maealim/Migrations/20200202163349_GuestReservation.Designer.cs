﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using maealim.Data;

namespace maealim.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200202163349_GuestReservation")]
    partial class GuestReservation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("maealim.Models.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArName");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("maealim.Models.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("maealim.Models.AppUserRole", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("maealim.Models.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("maealim.Models.College", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Colleges");
                });

            modelBuilder.Entity("maealim.Models.Continent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Continents");
                });

            modelBuilder.Entity("maealim.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContinentId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("ContinentId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("maealim.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("JopId");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("JopId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("maealim.Models.EmployeeContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DailyWorkingHours");

                    b.Property<int>("EmployeeId");

                    b.Property<float>("Extra");

                    b.Property<DateTime>("FromDate");

                    b.Property<float>("HourlyPay");

                    b.Property<int>("JopId");

                    b.Property<int>("SeasonId");

                    b.Property<bool>("Status");

                    b.Property<DateTime>("ToDate");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("JopId");

                    b.HasIndex("SeasonId");

                    b.ToTable("EmployeeContracts");
                });

            modelBuilder.Entity("maealim.Models.GuestReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Duration");

                    b.Property<int?>("MGuideId");

                    b.Property<DateTime>("ReservationDate");

                    b.Property<int?>("SessionNo");

                    b.Property<int?>("SheikhId");

                    b.Property<int?>("Status");

                    b.HasKey("Id");

                    b.HasIndex("MGuideId");

                    b.HasIndex("SheikhId");

                    b.ToTable("GuestReservations");
                });

            modelBuilder.Entity("maealim.Models.GuideContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DailyWorkingHours");

                    b.Property<float>("Extra");

                    b.Property<DateTime>("FromDate");

                    b.Property<int?>("GuideContractId");

                    b.Property<int>("GuideId");

                    b.Property<float>("HourlyPay");

                    b.Property<int>("JopId");

                    b.Property<int>("SeasonId");

                    b.Property<bool>("Status");

                    b.Property<DateTime>("ToDate");

                    b.HasKey("Id");

                    b.HasIndex("GuideContractId");

                    b.HasIndex("GuideId");

                    b.HasIndex("JopId");

                    b.HasIndex("SeasonId");

                    b.ToTable("GuideContracts");
                });

            modelBuilder.Entity("maealim.Models.ItemExport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int?>("ItemOfProductId")
                        .IsRequired();

                    b.Property<string>("Notce");

                    b.Property<int>("Qty");

                    b.Property<string>("Recipient")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ItemOfProductId");

                    b.ToTable("ItemExports");
                });

            modelBuilder.Entity("maealim.Models.ItemImport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int?>("ItemOfProductId")
                        .IsRequired();

                    b.Property<string>("Notce");

                    b.Property<int>("Qty");

                    b.Property<string>("Supplier")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ItemOfProductId");

                    b.ToTable("ItemImports");
                });

            modelBuilder.Entity("maealim.Models.ItemOfProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("TypeOfProductId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TypeOfProductId");

                    b.ToTable("ItemOfProducts");
                });

            modelBuilder.Entity("maealim.Models.JobNotable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TypeNotableId");

                    b.HasKey("Id");

                    b.HasIndex("TypeNotableId");

                    b.ToTable("JobNotable");
                });

            modelBuilder.Entity("maealim.Models.Jop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Jops");
                });

            modelBuilder.Entity("maealim.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("maealim.Models.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("maealim.Models.MGuide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AppUserId");

                    b.Property<int?>("BankId");

                    b.Property<int?>("CollegeId");

                    b.Property<int?>("CountryId");

                    b.Property<string>("Email");

                    b.Property<int?>("LanguageId");

                    b.Property<int?>("LevelId");

                    b.Property<string>("Mobile");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("SpecializationId");

                    b.Property<int?>("StageId");

                    b.Property<int?>("UniversityId");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("BankId");

                    b.HasIndex("CollegeId");

                    b.HasIndex("CountryId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("LevelId");

                    b.HasIndex("SpecializationId");

                    b.HasIndex("StageId");

                    b.HasIndex("UniversityId");

                    b.ToTable("MGuides");
                });

            modelBuilder.Entity("maealim.Models.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("maealim.Models.Sheikh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CardId")
                        .HasMaxLength(10);

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<string>("Job")
                        .HasMaxLength(100);

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(14);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("Volunteer");

                    b.HasKey("Id");

                    b.ToTable("Sheikhs");
                });

            modelBuilder.Entity("maealim.Models.Specialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Specializations");
                });

            modelBuilder.Entity("maealim.Models.Stage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Stages");
                });

            modelBuilder.Entity("maealim.Models.TypeNotable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("TypeNotables");
                });

            modelBuilder.Entity("maealim.Models.TypeOfProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("TypeOfProducts");
                });

            modelBuilder.Entity("maealim.Models.University", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("maealim.Models.WorkSeason", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("SeasonId");

                    b.Property<DateTime>("StartDate");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("WorkSeasons");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("maealim.Models.AppRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("maealim.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("maealim.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("maealim.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.AppUserRole", b =>
                {
                    b.HasOne("maealim.Models.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.Country", b =>
                {
                    b.HasOne("maealim.Models.Continent", "Continent")
                        .WithMany()
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.Employee", b =>
                {
                    b.HasOne("maealim.Models.Jop", "Jop")
                        .WithMany("Employees")
                        .HasForeignKey("JopId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.AppUser", "User")
                        .WithOne("Employee")
                        .HasForeignKey("maealim.Models.Employee", "UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.EmployeeContract", b =>
                {
                    b.HasOne("maealim.Models.Employee", "Employee")
                        .WithMany("EmployeeContracts")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.Jop", "Jop")
                        .WithMany("EmployeeContracts")
                        .HasForeignKey("JopId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.Season", "Season")
                        .WithMany("EmployeeContracts")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.GuestReservation", b =>
                {
                    b.HasOne("maealim.Models.MGuide", "MGuide")
                        .WithMany("GuestReservations")
                        .HasForeignKey("MGuideId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.Sheikh", "Sheikh")
                        .WithMany("GuestReservations")
                        .HasForeignKey("SheikhId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.GuideContract", b =>
                {
                    b.HasOne("maealim.Models.GuideContract")
                        .WithMany("GuideContracts")
                        .HasForeignKey("GuideContractId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.MGuide", "Guide")
                        .WithMany()
                        .HasForeignKey("GuideId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.Jop", "Jop")
                        .WithMany("GuideContracts")
                        .HasForeignKey("JopId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.Season", "Season")
                        .WithMany("GuideContracts")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.ItemExport", b =>
                {
                    b.HasOne("maealim.Models.ItemOfProduct", "ItemOfProduct")
                        .WithMany("ItemExports")
                        .HasForeignKey("ItemOfProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.ItemImport", b =>
                {
                    b.HasOne("maealim.Models.ItemOfProduct", "ItemOfProduct")
                        .WithMany("ItemImports")
                        .HasForeignKey("ItemOfProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.ItemOfProduct", b =>
                {
                    b.HasOne("maealim.Models.TypeOfProduct", "TypeOfProduct")
                        .WithMany("ItemOfProducts")
                        .HasForeignKey("TypeOfProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.JobNotable", b =>
                {
                    b.HasOne("maealim.Models.TypeNotable", "TypeNotable")
                        .WithMany("JobNotables")
                        .HasForeignKey("TypeNotableId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.MGuide", b =>
                {
                    b.HasOne("maealim.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.Bank", "Bank")
                        .WithMany("MGuides")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.College", "College")
                        .WithMany("MGuides")
                        .HasForeignKey("CollegeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.Country", "Country")
                        .WithMany("MGuides")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.Language", "Language")
                        .WithMany("MGuides")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.Level", "Level")
                        .WithMany("MGuides")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.Specialization", "Specialization")
                        .WithMany("MGuides")
                        .HasForeignKey("SpecializationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.Stage", "Stage")
                        .WithMany("MGuides")
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("maealim.Models.University", "University")
                        .WithMany("MGuides")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("maealim.Models.WorkSeason", b =>
                {
                    b.HasOne("maealim.Models.Season", "Season")
                        .WithMany()
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
