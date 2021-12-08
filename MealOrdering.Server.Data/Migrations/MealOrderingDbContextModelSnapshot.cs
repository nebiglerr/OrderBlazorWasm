﻿// <auto-generated />
using System;
using MealOrdering.Server.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MealOrdering.Server.Data.Migrations
{
    [DbContext(typeof(MealOrderingDbContext))]
    partial class MealOrderingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("MealOrdering.Server.Data.Models.OrderItems", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("createdate")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("CreatedUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user_id");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.HasKey("Id")
                        .HasName("pk_orderItem_id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("order_items", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Orders", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("createdate")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("CreatedUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("created_user_id");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying")
                        .HasColumnName("description");

                    b.Property<DateTime>("ExpireDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("expire_date");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<Guid>("SupplierdId")
                        .HasColumnType("uuid")
                        .HasColumnName("supplier_id");

                    b.HasKey("Id")
                        .HasName("pk_order_id");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("SupplierdId");

                    b.ToTable("orders", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Suppliers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("public.uuid_generate_v4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("createdate")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isactive");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.Property<string>("WebUrl")
                        .HasMaxLength(500)
                        .HasColumnType("character varying")
                        .HasColumnName("web_url");

                    b.HasKey("Id")
                        .HasName("pk_supplier_id");

                    b.ToTable("suppliers", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id")
                        .HasDefaultValueSql("UUID_GENERATE_V4()");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("create_date")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("EMailAdress")
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("email_address");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("isactive");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_user_id");

                    b.ToTable("user", "public");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.OrderItems", b =>
                {
                    b.HasOne("MealOrdering.Server.Data.Models.Users", "CreatedUser")
                        .WithMany("CreatedOrderItems")
                        .HasForeignKey("CreatedUserId")
                        .HasConstraintName("fk_orderitems_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MealOrdering.Server.Data.Models.Orders", "Orders")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("fk_orderitems_order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedUser");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Orders", b =>
                {
                    b.HasOne("MealOrdering.Server.Data.Models.Users", "CreatedUser")
                        .WithMany("Orders")
                        .HasForeignKey("CreatedUserId")
                        .HasConstraintName("fk_user_order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MealOrdering.Server.Data.Models.Suppliers", "Supplier")
                        .WithMany("Orders")
                        .HasForeignKey("SupplierdId")
                        .HasConstraintName("fk_supplier_order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedUser");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Orders", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Suppliers", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("MealOrdering.Server.Data.Models.Users", b =>
                {
                    b.Navigation("CreatedOrderItems");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
