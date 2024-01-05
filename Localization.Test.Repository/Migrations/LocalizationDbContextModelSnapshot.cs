﻿// <auto-generated />
using System;
using Localization.Test.Repository.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Localization.Test.Repository.Migrations
{
    [DbContext(typeof(LocalizationDbContext))]
    partial class LocalizationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Localization.Test.Infrastructure.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Localization.Test.Infrastructure.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("CreateAccess")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<bool>("DeleteAccess")
                        .HasColumnType("bit");

                    b.Property<bool>("EditAccess")
                        .HasColumnType("bit");

                    b.Property<int>("Feature")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("ViewAccess")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Localization.Test.Infrastructure.Models.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Culture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("TemplateBody")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TemplateHeader")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("Localization.Test.Infrastructure.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Culture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Localization.Test.Infrastructure.Models.Account", b =>
                {
                    b.HasOne("Localization.Test.Infrastructure.Models.User", "Creator")
                        .WithMany("AccountCreators")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Localization.Test.Infrastructure.Models.User", "Updator")
                        .WithMany("AccountUpdators")
                        .HasForeignKey("UpdatedBy");

                    b.HasOne("Localization.Test.Infrastructure.Models.User", "User")
                        .WithOne("Account")
                        .HasForeignKey("Localization.Test.Infrastructure.Models.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Updator");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Localization.Test.Infrastructure.Models.Role", b =>
                {
                    b.HasOne("Localization.Test.Infrastructure.Models.User", "Creator")
                        .WithMany("RoleCreators")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Localization.Test.Infrastructure.Models.User", "Updator")
                        .WithMany("RoleUpdators")
                        .HasForeignKey("UpdatedBy");

                    b.HasOne("Localization.Test.Infrastructure.Models.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Updator");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Localization.Test.Infrastructure.Models.Template", b =>
                {
                    b.HasOne("Localization.Test.Infrastructure.Models.User", "Creator")
                        .WithMany("TemlateCreators")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Localization.Test.Infrastructure.Models.User", "Updator")
                        .WithMany("TemplateUpdators")
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("Creator");

                    b.Navigation("Updator");
                });

            modelBuilder.Entity("Localization.Test.Infrastructure.Models.User", b =>
                {
                    b.HasOne("Localization.Test.Infrastructure.Models.User", "Creator")
                        .WithMany("UserCreators")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Localization.Test.Infrastructure.Models.User", "Updator")
                        .WithMany("UserUpdators")
                        .HasForeignKey("UpdatedBy");

                    b.Navigation("Creator");

                    b.Navigation("Updator");
                });

            modelBuilder.Entity("Localization.Test.Infrastructure.Models.User", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("AccountCreators");

                    b.Navigation("AccountUpdators");

                    b.Navigation("RoleCreators");

                    b.Navigation("RoleUpdators");

                    b.Navigation("Roles");

                    b.Navigation("TemlateCreators");

                    b.Navigation("TemplateUpdators");

                    b.Navigation("UserCreators");

                    b.Navigation("UserUpdators");
                });
#pragma warning restore 612, 618
        }
    }
}