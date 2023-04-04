using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ContabilidadAPI.Models;

public partial class zzContabilidadDbContext : DbContext
{
    public zzContabilidadDbContext()
    {
    }

    public zzContabilidadDbContext(DbContextOptions<zzContabilidadDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<zzfiCuentas> FiCuentas { get; set; }

    public virtual DbSet<zzfiCuentasNiveles> FiCuentasNiveles { get; set; }

    public virtual DbSet<zzfiDiarios> FiDiarios { get; set; }

    public virtual DbSet<zzfiDiariosDetalle> FiDiariosDetalles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<zzfiCuentas>(entity =>
        {
            entity.HasKey(e => e.CuentaId);

            entity.ToTable("fiCuentas");

            entity.Property(e => e.CuentaId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CuentaID");
            entity.Property(e => e.CuentaControlId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CuentaControlID");
            entity.Property(e => e.CuentaNombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NivelId).HasColumnName("NivelID");
            entity.Property(e => e.Origen)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d._fiNiveles).WithMany(p => p.FiCuenta)
                .HasForeignKey(d => d.NivelId)
                .HasConstraintName("FK__fiCuentas__Nivel__286302EC");
        });

        modelBuilder.Entity<zzfiCuentasNiveles>(entity =>
        {
            entity.HasKey(e => e.NivelId).HasName("PK__fiCuenta__316FA297354F57ED");

            entity.ToTable("fiCuentasNiveles");

            entity.Property(e => e.NivelId)
                .ValueGeneratedNever()
                .HasColumnName("NivelID");
            entity.Property(e => e.NivelNombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<zzfiDiarios>(entity =>
        {
            entity.HasKey(e => new { e.CompaniaId, e.OficinaId, e.TransaccionId });

            entity.ToTable("fiDiarios");

            entity.Property(e => e.CompaniaId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CompaniaID");
            entity.Property(e => e.OficinaId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OficinaID");
            entity.Property(e => e.TransaccionId).HasColumnName("TransaccionID");
            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Numero)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<zzfiDiarios>()
        .HasMany(d => d.Detalles)
        .WithOne()
        .HasForeignKey(d => new { d.CompaniaId, d.OficinaId, d.TransaccionId })
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<zzfiDiariosDetalle>(entity =>
        {
            entity.HasKey(e => new { e.CompaniaId, e.OficinaId, e.TransaccionId, e.TransaccionDetalleId });

            entity.ToTable("fiDiariosDetalle");

            entity.Property(e => e.CompaniaId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CompaniaID");
            entity.Property(e => e.OficinaId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("OficinaID");
            entity.Property(e => e.TransaccionId)
                .HasColumnName("TransaccionID");
            entity.Property(e => e.TransaccionDetalleId).HasColumnName("TransaccionDetalleID");
            entity.Property(e => e.Credito).HasColumnType("numeric(24, 8)");
            entity.Property(e => e.CuentaId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CuentaID");
            entity.Property(e => e.Debito).HasColumnType("numeric(24, 8)");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.FiDiariosDetalles)
                .HasForeignKey(d => d.CuentaId)
                .HasConstraintName("FK__fiDiarios__Cuent__2D27B809");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}