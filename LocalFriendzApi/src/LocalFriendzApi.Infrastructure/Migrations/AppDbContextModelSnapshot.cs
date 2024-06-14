﻿// <auto-generated />
using System;
using LocalFriendzApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LocalFriendzApi.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LocalFriendzApi.Core.Models.AreaCode", b =>
                {
                    b.Property<Guid>("IdAreaCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodeRegion")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("code_region");

                    b.HasKey("IdAreaCode")
                        .HasName("id_area_code");

                    b.ToTable("TB_AREA_CODE", (string)null);
                });

            modelBuilder.Entity("LocalFriendzApi.Core.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AreaCodeIdAreaCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR")
                        .HasColumnName("phone");

                    b.HasKey("Id")
                        .HasName("id_contact");

                    b.HasIndex("AreaCodeIdAreaCode");

                    b.ToTable("TB_CONTACT", (string)null);
                });

            modelBuilder.Entity("LocalFriendzApi.Core.Models.Contact", b =>
                {
                    b.HasOne("LocalFriendzApi.Core.Models.AreaCode", "AreaCode")
                        .WithMany()
                        .HasForeignKey("AreaCodeIdAreaCode");

                    b.Navigation("AreaCode");
                });
#pragma warning restore 612, 618
        }
    }
}
