// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tqviet.CoffeeShop.Models;

namespace Tqviet.CoffeeShop.Migrations
{
    [DbContext(typeof(CoffeeOrderDbContext))]
    [Migration("20210919031035_Coffee_V1.0.0.1_AddQuantity")]
    partial class Coffee_V1001_AddQuantity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tqviet.CoffeeShop.Models.CoffeeOrders", b =>
                {
                    b.Property<int>("CoffeeOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CoffeeOrderClientIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoffeeOrderDateTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CoffeeOrderQuantity")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("CoffeeOrderId");

                    b.ToTable("CoffeeOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
