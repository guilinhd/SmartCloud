﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartCloud.Common.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

#nullable disable

namespace SmartCloud.Common.Migrations
{
    [DbContext(typeof(CommonDbContext))]
    [Migration("20220610052657_InitDatabase")]
    partial class InitDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.MySql)
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SmartCloud.Common.Attachments.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("longtext")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastWriteTimeUtc")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Length")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ServerFileName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ServerPathName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TableId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("TableId");

                    b.HasIndex("TableId", "ServerFileName");

                    b.HasIndex("TableId", "ServerPathName");

                    b.ToTable("Attachment", (string)null);
                });

            modelBuilder.Entity("SmartCloud.Common.DataIndexs.DataIndex", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Editor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("longtext")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Reader")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("DataIndex", (string)null);
                });

            modelBuilder.Entity("SmartCloud.Common.Datas.Data", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("longtext")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("No")
                        .HasColumnType("int");

                    b.Property<string>("Remark1")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Remark10")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark11")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark12")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark13")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark14")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark15")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark3")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark4")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark5")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark6")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark7")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark8")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark9")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Category");

                    b.HasIndex("Category", "Name");

                    b.HasIndex("Category", "Remark1");

                    b.HasIndex("Category", "Name", "Remark1");

                    b.ToTable("Data", (string)null);
                });

            modelBuilder.Entity("SmartCloud.Common.Organizations.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Accounting")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("longtext")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("Fax")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("No")
                        .HasColumnType("int");

                    b.Property<string>("ParentId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Accounting");

                    b.HasIndex("Category");

                    b.HasIndex("Name");

                    b.HasIndex("ParentId");

                    b.HasIndex("Type");

                    b.ToTable("Organization", (string)null);
                });

            modelBuilder.Entity("SmartCloud.Common.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("ConcurrencyStamp");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Disable")
                        .HasColumnType("int");

                    b.Property<string>("ExtraProperties")
                        .HasColumnType("longtext")
                        .HasColumnName("ExtraProperties");

                    b.Property<string>("Fax")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("No")
                        .HasColumnType("int");

                    b.Property<string>("OrganizationId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Post")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Pwd")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PwdSalt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("Post");

                    b.ToTable("User", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
