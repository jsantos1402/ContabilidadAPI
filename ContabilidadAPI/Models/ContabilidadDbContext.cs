﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ContabilidadAPI.Models;

public partial class ContabilidadDbContext : DbContext
{
    public ContabilidadDbContext()
    {
    }

    public ContabilidadDbContext(DbContextOptions<ContabilidadDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<fiCuentas> FiCuentas { get; set; }

    public virtual DbSet<fiCuentasNiveles> FiCuentasNiveles { get; set; }

    public virtual DbSet<fiDiarios> FiDiarios { get; set; }

    public virtual DbSet<fiDiariosDetalle> FiDiariosDetalles { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<RolesAccesos> RolesAccesos { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<fiCuentas>(entity =>
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
                .HasConstraintName("FK__fiCuentas__Nivel__2A4B4B5E");
        });

        modelBuilder.Entity<fiCuentasNiveles>(entity =>
        {
            entity.HasKey(e => e.NivelId).HasName("PK__fiCuenta__316FA2976A20AA9C");

            entity.ToTable("fiCuentasNiveles");

            entity.Property(e => e.NivelId)
                .ValueGeneratedNever()
                .HasColumnName("NivelID");
            entity.Property(e => e.NivelNombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<fiDiarios>(entity =>
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

        modelBuilder.Entity<fiDiarios>()
        .HasMany(d => d.Detalles)
        .WithOne()
        .HasForeignKey(d => new { d.CompaniaId, d.OficinaId, d.TransaccionId })
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<fiDiariosDetalle>(entity =>
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
            entity.Property(e => e.TransaccionId).HasColumnName("TransaccionID");
            entity.Property(e => e.TransaccionDetalleId).HasColumnName("TransaccionDetalleID");
            entity.Property(e => e.Credito).HasColumnType("numeric(24, 8)");
            entity.Property(e => e.CuentaId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CuentaID");
            entity.Property(e => e.Debito).HasColumnType("numeric(24, 8)");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.FiDiariosDetalles)
                .HasForeignKey(d => d.CuentaId)
                .HasConstraintName("FK__fiDiarios__Cuent__2B3F6F97");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302D116FCDDFD");

            entity.Property(e => e.RolId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RolID");
            entity.Property(e => e.Estatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RolNombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolesAccesos>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__RolesAcc__F92302D132299987");

            entity.Property(e => e.RolId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RolID");

            entity.HasOne(d => d.Rol).WithOne(p => p.RolesAcceso)
                .HasForeignKey<RolesAccesos>(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolesAcce__RolID__37A5467C");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798239E184A");

            entity.Property(e => e.UsuarioId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("UsuarioID");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.RolId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RolID");
            entity.Property(e => e.UsuarioNombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Usuarios__RolID__34C8D9D1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
