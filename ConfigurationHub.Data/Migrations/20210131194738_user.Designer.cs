﻿// <auto-generated />
using System;
using Configuration.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ConfigurationHub.Data.Migrations
{
    [DbContext(typeof(ConfigurationContext))]
    [Migration("20210131194738_user")]
    partial class user
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ConfigurationHub.Domain.Auth.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("BLOB");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ConfigurationHub.Domain.Config", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MicroserviceId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("LastModified");

                    b.HasIndex("MicroserviceId");

                    b.ToTable("Configs");
                });

            modelBuilder.Entity("ConfigurationHub.Domain.ConfigContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfigId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ConfigId")
                        .IsUnique();

                    b.ToTable("ConfigContents");
                });

            modelBuilder.Entity("ConfigurationHub.Domain.Microservice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SystemId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("SystemId");

                    b.ToTable("MicroServices");
                });

            modelBuilder.Entity("ConfigurationHub.Domain.System", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Systems");
                });

            modelBuilder.Entity("ConfigurationHub.Domain.Config", b =>
                {
                    b.HasOne("ConfigurationHub.Domain.Auth.User", "Author")
                        .WithMany("Configs")
                        .HasForeignKey("AuthorId");

                    b.HasOne("ConfigurationHub.Domain.Microservice", "Microservice")
                        .WithMany("Configs")
                        .HasForeignKey("MicroserviceId");

                    b.Navigation("Author");

                    b.Navigation("Microservice");
                });

            modelBuilder.Entity("ConfigurationHub.Domain.ConfigContent", b =>
                {
                    b.HasOne("ConfigurationHub.Domain.Config", "Config")
                        .WithOne("ConfigContent")
                        .HasForeignKey("ConfigurationHub.Domain.ConfigContent", "ConfigId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Config");
                });

            modelBuilder.Entity("ConfigurationHub.Domain.Microservice", b =>
                {
                    b.HasOne("ConfigurationHub.Domain.System", "System")
                        .WithMany("Microservices")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("System");
                });

            modelBuilder.Entity("ConfigurationHub.Domain.Auth.User", b =>
                {
                    b.Navigation("Configs");
                });

            modelBuilder.Entity("ConfigurationHub.Domain.Config", b =>
                {
                    b.Navigation("ConfigContent");
                });

            modelBuilder.Entity("ConfigurationHub.Domain.Microservice", b =>
                {
                    b.Navigation("Configs");
                });

            modelBuilder.Entity("ConfigurationHub.Domain.System", b =>
                {
                    b.Navigation("Microservices");
                });
#pragma warning restore 612, 618
        }
    }
}
