﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleIdentityServer.UI.Models;

#nullable disable

namespace SampleIdentityServer.UI.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230104165711_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SampleIdentityServer.UI.Models.CustomUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("customUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "İstanbul",
                            Email = "cnrgrsc@gmail.com",
                            Password = "password",
                            UserName = "cnrgrsc"
                        },
                        new
                        {
                            Id = 2,
                            City = "Erzurum",
                            Email = "ogzgrsc@gmail.com",
                            Password = "password",
                            UserName = "cnrgrsc"
                        },
                        new
                        {
                            Id = 3,
                            City = "İzmir",
                            Email = "hsngrsc@gmail.com",
                            Password = "password",
                            UserName = "cnrgrsc"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
