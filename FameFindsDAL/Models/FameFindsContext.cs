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

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerPasswordResetToken> CustomerPasswordResetTokens { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Shop> Shops { get; set; }

    public virtual DbSet<ShopProduct> ShopProducts { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<VendorPasswordResetToken> VendorPasswordResetTokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder
 optionsBuilder)
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
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B7944B1BA");

            entity.ToTable("Category");

            entity.HasIndex(e => e.CategoryName, "UQ__Category__8517B2E07DBF3426").IsUnique();

            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__City__F2D21B761E196F32");

            entity.ToTable("City");

            entity.HasIndex(e => e.CityName, "UQ__City__886159E5E63D849C").IsUnique();

            entity.Property(e => e.CityId).ValueGeneratedNever();
            entity.Property(e => e.CityName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8F110EF1C");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Email, "UQ__Customer__A9D10534C49C04E9").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<CustomerPasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07A17EF13B");

            entity.HasIndex(e => e.Token, "UQ__Customer__1EB4F817E59463FD").IsUnique();

            entity.Property(e => e.Expiry).HasColumnType("datetime");
            entity.Property(e => e.IsUsed).HasDefaultValue(false);
            entity.Property(e => e.RequestedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(255);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerPasswordResetTokens)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CustomerP__Custo__2D27B809");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD884653A1");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(150);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__412EB0B6");

            entity.HasOne(d => d.City).WithMany(p => p.Products)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__Products__CityId__4222D4EF");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Rating__FCCDF87C65C2E359");

            entity.ToTable("Rating");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Review).HasMaxLength(500);

            entity.HasOne(d => d.Customer).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Rating__Customer__4AB81AF0");

            entity.HasOne(d => d.Shop).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("FK__Rating__ShopId__4BAC3F29");
        });

        modelBuilder.Entity<Shop>(entity =>
        {
            entity.HasKey(e => e.ShopId).HasName("PK__Shop__67C557C9FB3729D3");

            entity.ToTable("Shop");

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

            entity.HasOne(d => d.City).WithMany(p => p.Shops)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__Shop__CityId__3B75D760");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Shops)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shop__VendorId__3A81B327");
        });

        modelBuilder.Entity<ShopProduct>(entity =>
        {
            entity.HasKey(e => e.ShopProductId).HasName("PK__ShopProd__A9FBB4D5BADA2DE3");

            entity.ToTable("ShopProduct");

            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.ShopProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ShopProdu__Produ__45F365D3");

            entity.HasOne(d => d.Shop).WithMany(p => p.ShopProducts)
                .HasForeignKey(d => d.ShopId)
                .HasConstraintName("FK__ShopProdu__ShopI__44FF419A");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PK__Vendor__FC8618F39D12C1FF");

            entity.ToTable("Vendor");

            entity.HasIndex(e => e.Email, "UQ__Vendor__A9D10534F691F7C2").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.VendorName).HasMaxLength(100);
        });

        modelBuilder.Entity<VendorPasswordResetToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VendorPa__3214EC07D4071888");

            entity.HasIndex(e => e.Token, "UQ__VendorPa__1EB4F817A1666AA8").IsUnique();

            entity.Property(e => e.Expiry).HasColumnType("datetime");
            entity.Property(e => e.IsUsed).HasDefaultValue(false);
            entity.Property(e => e.RequestedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Token).HasMaxLength(255);

            entity.HasOne(d => d.Vendor).WithMany(p => p.VendorPasswordResetTokens)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VendorPas__Vendo__32E0915F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
