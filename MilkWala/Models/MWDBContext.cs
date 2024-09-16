using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MilkWala.Models;

public partial class MWDBContext : DbContext
{
    public MWDBContext()
    {
    }

    public MWDBContext(DbContextOptions<MWDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MwCustomer> MwCustomers { get; set; }
    public virtual DbSet<MWDeliveryBoy> MWDeliveryBoys { get; set; }
    public virtual DbSet<MWOwner> MWOwners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=idl-milk-app.database.windows.net;Database=Milk_App;User Id=IDL;Password=G@pu7comLalu;Trusted_Connection=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MwCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MW_CUSTO__3214EC07ABB013F8");

            entity.ToTable("MW_CUSTOMER");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        modelBuilder.Entity<MWDeliveryBoy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Delivery__3214EC07C3CB79E2");

            entity.ToTable("MW_DELIVERYBOY");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
        });

        modelBuilder.Entity<MWOwner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MW_OWNER__3214EC07A5A95B1A");

            entity.ToTable("MW_OWNER");

            entity.Property(e => e.Id)
                .HasColumnName("Id")
                .HasDefaultValueSql("newid()");

            entity.Property(e => e.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Phone)
                .HasColumnName("Phone")
                .HasMaxLength(15)
                .IsRequired();

            entity.Property(e => e.Email)
                .HasColumnName("Email")
                .HasMaxLength(100);

            entity.Property(e => e.IsActive)
                .HasColumnName("IsActive");

            entity.Property(e => e.OTP)
                .HasColumnName("OTP")
                .HasMaxLength(100);

            entity.Property(e => e.OTPGenTime)
                .HasColumnName("OTP_GEN_TIME");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
