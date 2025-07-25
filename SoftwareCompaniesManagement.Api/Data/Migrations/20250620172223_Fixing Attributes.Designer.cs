﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SoftwareCompaniesManagement.Api.Data;

#nullable disable

namespace SoftwareCompaniesManagement.Api.Data.Migrations
{
    [DbContext(typeof(CompaniesContext))]
    [Migration("20250620172223_Fixing Attributes")]
    partial class FixingAttributes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EstablishDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Developer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("HiringDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Salary")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.DeveloperProject", b =>
                {
                    b.Property<int>("DeveloperId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("WorkHours")
                        .HasColumnType("REAL");

                    b.HasKey("DeveloperId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("DeveloperProjects");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.DeveloperTechnology", b =>
                {
                    b.Property<int>("DeveloperId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TechnologyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExperienceLevel")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("ExperienceYears")
                        .HasColumnType("REAL");

                    b.Property<double>("Points")
                        .HasColumnType("REAL");

                    b.HasKey("DeveloperId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("DeveloperTechnologies");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("TEXT");

                    b.Property<int>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("HiringDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Salary")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ManagerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProjectPoints")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.ProjectTechnology", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TechnologyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProjectId", "TechnologyId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("ProjectTechnologies");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("ActualEffort")
                        .HasColumnType("REAL");

                    b.Property<int>("Complexity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DeveloperId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("EstimateEffort")
                        .HasColumnType("REAL");

                    b.Property<int>("Priority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TechnologyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("TechnologyName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Company", b =>
                {
                    b.HasOne("SoftwareCompaniesManagement.Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Developer", b =>
                {
                    b.HasOne("SoftwareCompaniesManagement.Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareCompaniesManagement.Model.Company", "Company")
                        .WithMany("Developers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.DeveloperProject", b =>
                {
                    b.HasOne("SoftwareCompaniesManagement.Model.Developer", "Developer")
                        .WithMany("DeveloperProjects")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareCompaniesManagement.Model.Project", "Project")
                        .WithMany("DeveloperProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.DeveloperTechnology", b =>
                {
                    b.HasOne("SoftwareCompaniesManagement.Model.Developer", "Developer")
                        .WithMany("DeveloperTechnologies")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareCompaniesManagement.Model.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("Technology");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Employee", b =>
                {
                    b.HasOne("SoftwareCompaniesManagement.Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareCompaniesManagement.Model.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Project", b =>
                {
                    b.HasOne("SoftwareCompaniesManagement.Model.Company", "Company")
                        .WithMany("Projects")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareCompaniesManagement.Model.Employee", "Manager")
                        .WithMany("Projects")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.ProjectTechnology", b =>
                {
                    b.HasOne("SoftwareCompaniesManagement.Model.Project", "Project")
                        .WithMany("ProjectTechnologies")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareCompaniesManagement.Model.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Technology");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Task", b =>
                {
                    b.HasOne("SoftwareCompaniesManagement.Model.Developer", "Developer")
                        .WithMany("Tasks")
                        .HasForeignKey("DeveloperId");

                    b.HasOne("SoftwareCompaniesManagement.Model.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SoftwareCompaniesManagement.Model.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");

                    b.Navigation("Project");

                    b.Navigation("Technology");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Company", b =>
                {
                    b.Navigation("Developers");

                    b.Navigation("Employees");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Developer", b =>
                {
                    b.Navigation("DeveloperProjects");

                    b.Navigation("DeveloperTechnologies");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Employee", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("SoftwareCompaniesManagement.Model.Project", b =>
                {
                    b.Navigation("DeveloperProjects");

                    b.Navigation("ProjectTechnologies");

                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
