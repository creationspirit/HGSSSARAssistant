﻿// <auto-generated />
using HGSSSARAssistant.Core;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HGSSSARAssistant.DAL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20180126134520_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("HGSSSARAssistant.Core.Action", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ActionTypeId");

                    b.Property<string>("Description");

                    b.Property<long?>("LeaderId");

                    b.Property<long?>("LocationId");

                    b.Property<DateTime>("MeetupTime");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ActionTypeId");

                    b.HasIndex("LeaderId");

                    b.HasIndex("LocationId");

                    b.ToTable("Actions");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.ActionType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ActionTypes");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.Availability", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Day");

                    b.Property<DateTime>("EndTime");

                    b.Property<long?>("LocationId");

                    b.Property<DateTime>("StartTime");

                    b.Property<long?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("UserId");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.Expertise", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Expertises");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.Location", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<decimal>("Latitude");

                    b.Property<decimal>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.MessageTemplate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Message");

                    b.HasKey("Id");

                    b.ToTable("MessageTemplates");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.Station", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("ActionId");

                    b.Property<long?>("ActionId1");

                    b.Property<string>("AdditionalContactNumbers");

                    b.Property<long?>("AddressId");

                    b.Property<string>("AndroidPushId");

                    b.Property<long?>("CategoryId");

                    b.Property<string>("ContactNumber");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<long?>("RoleId");

                    b.Property<long?>("StationId");

                    b.HasKey("Id");

                    b.HasIndex("ActionId");

                    b.HasIndex("ActionId1");

                    b.HasIndex("AddressId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RoleId");

                    b.HasIndex("StationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.UserExpertise", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("ExpertiseId");

                    b.HasKey("UserId", "ExpertiseId");

                    b.HasIndex("ExpertiseId");

                    b.ToTable("UserExpertise");
                });

            modelBuilder.Entity("HGSSSARAssistant.DAL.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

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
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.Action", b =>
                {
                    b.HasOne("HGSSSARAssistant.Core.ActionType", "ActionType")
                        .WithMany()
                        .HasForeignKey("ActionTypeId");

                    b.HasOne("HGSSSARAssistant.Core.User", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId");

                    b.HasOne("HGSSSARAssistant.Core.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.Availability", b =>
                {
                    b.HasOne("HGSSSARAssistant.Core.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("HGSSSARAssistant.Core.User")
                        .WithMany("Availiabilities")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.Station", b =>
                {
                    b.HasOne("HGSSSARAssistant.Core.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.User", b =>
                {
                    b.HasOne("HGSSSARAssistant.Core.Action")
                        .WithMany("AttendedRescuers")
                        .HasForeignKey("ActionId");

                    b.HasOne("HGSSSARAssistant.Core.Action")
                        .WithMany("InvitedRescuers")
                        .HasForeignKey("ActionId1");

                    b.HasOne("HGSSSARAssistant.Core.Location", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("HGSSSARAssistant.Core.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("HGSSSARAssistant.Core.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("HGSSSARAssistant.Core.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationId");
                });

            modelBuilder.Entity("HGSSSARAssistant.Core.UserExpertise", b =>
                {
                    b.HasOne("HGSSSARAssistant.Core.Expertise", "Expertise")
                        .WithMany("UserExpertise")
                        .HasForeignKey("ExpertiseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HGSSSARAssistant.Core.User", "User")
                        .WithMany("UserExpertise")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HGSSSARAssistant.DAL.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HGSSSARAssistant.DAL.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HGSSSARAssistant.DAL.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HGSSSARAssistant.DAL.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
