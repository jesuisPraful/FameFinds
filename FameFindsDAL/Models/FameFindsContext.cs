using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FameFindsDAL.Models;

public partial class FameFindsContext : DbContext
{
    public FameFindsContext()
    {
    }

    public FameFindsContext(DbContextOptions<FameFindsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShopProduct> ShopProducts { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString =
           config.GetConnectionString("ConnectionName");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B82EE1F42");

            entity.ToTable("Category");

            entity.HasIndex(e => e.CategoryName, "UQ__Category__8517B2E0BA6392F6").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D87A3E66C5");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D1053461462BB4").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD9453B435");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(150);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__32E0915F");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Rating__FCCDF87CB43E5F87");

            entity.ToTable("Rating");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Review).HasMaxLength(500);

            entity.HasOne(d => d.Customer).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Rating__Customer__3B75D760");

            entity.HasOne(d => d.Shop).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("FK__Rating__ShopId__3C69FB99");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK__Shop__67C557C933EBBF58");

            entity.ToTable("Shop");

            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullAddress)
                .HasColumnType("text")
                .HasColumnName("Full_Address");
            entity.Property(e => e.IsOpen).HasDefaultValue(true);
            entity.Property(e => e.Latitude).HasColumnType("decimal(10, 8)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(11, 8)");
            entity.Property(e => e.OpeningTime).HasColumnName("Opening_time");
            entity.Property(e => e.Pincode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("PINCODE");
            entity.Property(e => e.ShopName)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Vendor).WithMany(p => p.Shops)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shop__VendorId__2C3393D0");
        });

        modelBuilder.Entity<ShopProduct>(entity =>
        {
            entity.HasKey(e => e.ShopProductId).HasName("PK__ShopProd__A9FBB4D5C11DFD61");

            entity.ToTable("ShopProduct");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.ShopProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ShopProdu__Produ__36B12243");

            entity.HasOne(d => d.Shop).WithMany(p => p.ShopProducts)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("FK__ShopProdu__ShopI__35BCFE0A");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendor__FC8618F3EE270827");

            entity.ToTable("Vendor");

            entity.HasIndex(e => e.Email, "UQ__Vendor__A9D1053417C2ACD4").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.VendorName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
