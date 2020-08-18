﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RozgrywkiAkademickie4.Models;

namespace RozgrywkiAkademickie4.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200812165511_DodanieCzyBonus")]
    partial class DodanieCzyBonus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RozgrywkiAkademickie4.Models.Kierunek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CzyBonus")
                        .HasColumnType("bit");

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rok")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Kierunki");
                });

            modelBuilder.Entity("RozgrywkiAkademickie4.Models.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nazwa")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sporty");
                });

            modelBuilder.Entity("RozgrywkiAkademickie4.Models.Wynik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("KierunekId")
                        .HasColumnType("int");

                    b.Property<int>("Miejsce")
                        .HasColumnType("int");

                    b.Property<int>("Punkty")
                        .HasColumnType("int");

                    b.Property<int?>("ZawodyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KierunekId");

                    b.HasIndex("ZawodyId");

                    b.ToTable("Wyniki");
                });

            modelBuilder.Entity("RozgrywkiAkademickie4.Models.Zawody", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataZawodow")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SportId")
                        .HasColumnType("int");

                    b.Property<string>("ZdjecieUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("Zawody");
                });

            modelBuilder.Entity("RozgrywkiAkademickie4.Models.Wynik", b =>
                {
                    b.HasOne("RozgrywkiAkademickie4.Models.Kierunek", "Kierunek")
                        .WithMany()
                        .HasForeignKey("KierunekId");

                    b.HasOne("RozgrywkiAkademickie4.Models.Zawody", "Zawody")
                        .WithMany()
                        .HasForeignKey("ZawodyId");
                });

            modelBuilder.Entity("RozgrywkiAkademickie4.Models.Zawody", b =>
                {
                    b.HasOne("RozgrywkiAkademickie4.Models.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("SportId");
                });
#pragma warning restore 612, 618
        }
    }
}