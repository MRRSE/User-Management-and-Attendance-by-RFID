using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Gym.Models;

public partial class GymContext : DbContext
{
    public GymContext()
    {
    }

    public GymContext(DbContextOptions<GymContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminUser> AdminUsers { get; set; }

    public virtual DbSet<Man> Men { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=gym;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasKey(e => e.Adminid);

            entity.ToTable("admin_user");

            entity.Property(e => e.Adminid).HasColumnName("adminid");
            entity.Property(e => e.Accessibility).HasColumnName("accessibility");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("username");
        });

        modelBuilder.Entity<Man>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_man");

            entity.ToTable("men");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("age");
            entity.Property(e => e.Classes)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("classes");
            entity.Property(e => e.Date)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("date");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.Lastsing)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("lastsing");
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("lname");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("number");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Uid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("uid");
        });

        modelBuilder.Entity<UserLog>(entity =>
        {
            entity.HasKey(e => e.Logid);

            entity.ToTable("user_log");

            entity.Property(e => e.Logid).HasColumnName("logid");
            entity.Property(e => e.Enterydate)
                .HasColumnType("datetime")
                .HasColumnName("enterydate");
            entity.Property(e => e.Enterytime)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("enterytime");
            entity.Property(e => e.Exitdate)
                .HasColumnType("datetime")
                .HasColumnName("exitdate");
            entity.Property(e => e.Exittime)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("exittime");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
