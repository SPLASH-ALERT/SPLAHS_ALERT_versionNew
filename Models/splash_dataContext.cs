using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace splash_alert.Models
{
    public partial class splash_dataContext : DbContext
    {
        public splash_dataContext()
        {
        }

        public splash_dataContext(DbContextOptions<splash_dataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Causa> Causas { get; set; } = null!;
        public virtual DbSet<FechaActivacion> FechaActivacions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Causa>(entity =>
            {
                entity.HasKey(e => e.IdCausa)
                    .HasName("PK__causa__D200EA9BC509BF90");

                entity.ToTable("causa");

                entity.Property(e => e.IdCausa).HasColumnName("id_causa");

                entity.Property(e => e.Causa1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("causa");

                entity.Property(e => e.FechaCausa)
                    .HasColumnType("datetime2(7)")
                    .HasColumnName("fecha_causa");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.IdUsuarios)
                    .WithMany(p => p.Causas)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK_IDUSUARIOS");
            });

            modelBuilder.Entity<FechaActivacion>(entity =>
            {
                entity.HasKey(e => e.IdActivacion)
                    .HasName("PK__fecha_ac__D05D9150630896E2");

                entity.ToTable("fecha_activacion");

                entity.Property(e => e.IdActivacion).HasColumnName("id_activacion");

                entity.Property(e => e.FechaActivacion1)
                    .HasColumnType("datetime2(7)")
                    .HasColumnName("fecha_activacion");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdrRoles)
                    .HasName("PK__roles__22035A134935FDB7");

                entity.ToTable("roles");

                entity.Property(e => e.IdrRoles).HasColumnName("idr_roles");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_rol");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__usuarios__4E3E04AD01DEDB62");

                entity.ToTable("usuarios");

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Correo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("correo");

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estado");

                entity.Property(e => e.FechaCreacion)
                   .HasColumnType("datetime2(7)")
                   .HasColumnName("fecha_creacion");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre_usuario");

                entity.Property(e => e.Tokken)
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("tokken");

                entity.HasOne(d => d.IdRoles)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK_id_roles");


              


            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
