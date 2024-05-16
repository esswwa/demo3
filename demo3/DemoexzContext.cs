using System;
using System.Collections.Generic;
using demo3.Models;
using Microsoft.EntityFrameworkCore;

namespace demo3;

public partial class DemoexzContext : DbContext
{
    public DemoexzContext()
    {
    }

    public DemoexzContext(DbContextOptions<DemoexzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Executor> Executors { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TypeMalfunction> TypeMalfunctions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("host=localhost;user=root;password=1234;database=demoexz", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.IdApplication).HasName("PRIMARY");

            entity.ToTable("application");

            entity.HasIndex(e => e.IdEquipment, "fk_id_equipment_application_idx");

            entity.HasIndex(e => e.IdTypeMalfunction, "fk_id_type_malfunction_application_idx");

            entity.Property(e => e.IdApplication)
                .ValueGeneratedNever()
                .HasColumnName("id_application");
            entity.Property(e => e.CommentExecutor)
                .HasMaxLength(450)
                .HasColumnName("comment_executor");
            entity.Property(e => e.DateAddApplication).HasColumnName("date_add_application");
            entity.Property(e => e.DateEndApplication).HasColumnName("date_end_application");
            entity.Property(e => e.Description)
                .HasMaxLength(450)
                .HasColumnName("description");
            entity.Property(e => e.FullName)
                .HasMaxLength(450)
                .HasColumnName("full_name");
            entity.Property(e => e.IdEquipment).HasColumnName("id_equipment");
            entity.Property(e => e.IdTypeMalfunction).HasColumnName("id_type_malfunction");
            entity.Property(e => e.PhaseComplete)
                .HasMaxLength(45)
                .HasColumnName("phase_complete");
            entity.Property(e => e.PriceApplication).HasColumnName("price_application");
            entity.Property(e => e.StatusApplication)
                .HasMaxLength(45)
                .HasColumnName("status_application");
            entity.Property(e => e.TimeComplete)
                .HasMaxLength(450)
                .HasColumnName("time_complete");

            entity.HasOne(d => d.IdEquipmentNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.IdEquipment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_equipment_application");

            entity.HasOne(d => d.IdTypeMalfunctionNavigation).WithMany(p => p.Applications)
                .HasForeignKey(d => d.IdTypeMalfunction)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_type_malfunction_application");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.IdEquipment).HasName("PRIMARY");

            entity.ToTable("equipment");

            entity.Property(e => e.IdEquipment)
                .ValueGeneratedNever()
                .HasColumnName("id_equipment");
            entity.Property(e => e.EquipmentName)
                .HasMaxLength(450)
                .HasColumnName("equipment_name");
        });

        modelBuilder.Entity<Executor>(entity =>
        {
            entity.HasKey(e => e.IdExecutor).HasName("PRIMARY");

            entity.ToTable("executor");

            entity.HasIndex(e => e.IdApplication, "fk_id_application_idx");

            entity.HasIndex(e => e.IdUser, "fk_id_user_idx");

            entity.Property(e => e.IdExecutor)
                .ValueGeneratedNever()
                .HasColumnName("id_executor");
            entity.Property(e => e.IdApplication).HasColumnName("id_application");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdApplicationNavigation).WithMany(p => p.Executors)
                .HasForeignKey(d => d.IdApplication)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_application");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Executors)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_user");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.IdRole)
                .ValueGeneratedNever()
                .HasColumnName("id_role");
            entity.Property(e => e.RoleName)
                .HasMaxLength(45)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<TypeMalfunction>(entity =>
        {
            entity.HasKey(e => e.IdTypeMalfunction).HasName("PRIMARY");

            entity.ToTable("type_malfunction");

            entity.Property(e => e.IdTypeMalfunction)
                .ValueGeneratedNever()
                .HasColumnName("id_type_malfunction");
            entity.Property(e => e.TypeMalfunctionName)
                .HasMaxLength(450)
                .HasColumnName("type_malfunction_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.IdRole, "fk_id_role_idx");

            entity.Property(e => e.IdUser)
                .ValueGeneratedNever()
                .HasColumnName("id_user");
            entity.Property(e => e.FullName)
                .HasMaxLength(450)
                .HasColumnName("full_name");
            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Login)
                .HasMaxLength(450)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(450)
                .HasColumnName("password");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
