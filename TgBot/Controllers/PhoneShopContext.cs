using System;
using System.Collections.Generic;
using JWT_AspCoreApi;
using Microsoft.EntityFrameworkCore;
using TgBot.Models;

namespace TgBot.Controllers;

public partial class PhoneShopContext : DbContext {
    public PhoneShopContext() {
    }

    public PhoneShopContext(DbContextOptions<PhoneShopContext> options)
        : base(options) {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<Producer> Producers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { 
         optionsBuilder.UseSqlServer(JWT_AspCoreApi.ConfigurationManager.AppSetting.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Category>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC077226B607");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Client>(entity => {
            entity.HasKey(e => e.UserId).HasName("PK__Client__1788CC4CE4A3A00B");

            entity.ToTable("Client");

            entity.Property(e => e.TelegramId).HasMaxLength(200);
            entity.Property(e => e.Username).HasMaxLength(200);
        });

        modelBuilder.Entity<Order>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Order__3214EC075678488A");

            entity.ToTable("Order");

            entity.Property(e => e.Status).HasMaxLength(100);

            entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__Order__ClientId__5FB337D6");

            entity.HasOne(d => d.Phone).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PhoneId)
                .HasConstraintName("FK__Order__PhoneId__5EBF139D");
        });

        modelBuilder.Entity<Phone>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Phone__3213E83F47A219A3");

            entity.ToTable("Phone");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.PriceType).HasMaxLength(10);

            entity.HasOne(d => d.Category).WithMany(p => p.Phones)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Phone__CategoryI__3C69FB99");

            entity.HasOne(d => d.Producer).WithMany(p => p.Phones)
                .HasForeignKey(d => d.ProducerId)
                .HasConstraintName("FK__Phone__ProducerI__3D5E1FD2");
        });

        modelBuilder.Entity<Producer>(entity => {
            entity.HasKey(e => e.Id).HasName("PK__Producer__3214EC07C4486CEE");

            entity.ToTable("Producer");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
