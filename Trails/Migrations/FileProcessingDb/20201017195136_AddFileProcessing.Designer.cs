﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trails.Models;

namespace trails.Migrations.FileProcessingDb
{
    [DbContext(typeof(FileProcessingDbContext))]
    [Migration("20201017195136_AddFileProcessing")]
    partial class AddFileProcessing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Trails.FileProcessing.Models.AppliedFileTransforms", b =>
                {
                    b.Property<int>("fileId")
                        .HasColumnType("int");

                    b.Property<int>("transformJobId")
                        .HasColumnType("int");

                    b.Property<string>("appliedTransforms")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("transformName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("fileId", "transformJobId");

                    b.ToTable("AppliedFileTransforms");
                });

            modelBuilder.Entity("Trails.FileProcessing.Models.FileTransform", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("context")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("endTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("fileType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("s3Location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("startTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.ToTable("FileTransforms");
                });

            modelBuilder.Entity("Trails.FileProcessing.Models.TransformJob", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("context")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("endTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("fileId")
                        .HasColumnType("int");

                    b.Property<string>("input")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("startTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("transform")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("id");

                    b.ToTable("Transforms");
                });
#pragma warning restore 612, 618
        }
    }
}
