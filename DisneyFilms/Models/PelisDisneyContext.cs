using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace DisneyFilms.Models
{
    public partial class PelisDisneyContext : DbContext
    {
        public PelisDisneyContext()
        {
        }

        public PelisDisneyContext(DbContextOptions<PelisDisneyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<Personaje> Personajes { get; set; }
        public virtual DbSet<PersonajeFilm> PersonajeFilms { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data source=[servidor]; Initial Catalog=[Basededatos]; user id=[Usuario]; password=[Contraseña];");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Film>(entity =>
            {
                entity.HasKey(e => e.Titulo)
                    .HasName("PK__Film__7B406B573EBB8D63");

                entity.ToTable("Film");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.GeneroN)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.HasOne(d => d.GeneroNNavigation)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.GeneroN)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_GeneroN");
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.Nombre)
                    .HasName("PK__Genero__75E3EFCEE3F6F281");

                entity.ToTable("Genero");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Imagen).HasColumnType("image");
            });

            modelBuilder.Entity<Personaje>(entity =>
            {
                entity.HasKey(e => e.Nombre)
                    .HasName("PK__Personaj__75E3EFCE93EE1B35");

                entity.ToTable("Personaje");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Historia).IsUnicode(false);

                entity.Property(e => e.Imagen).HasColumnType("image");
            });

            modelBuilder.Entity<PersonajeFilm>(entity =>
            {
                entity.ToTable("PersonajeFilm");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Nfilm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NFilm");

                entity.Property(e => e.Npersonaje)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NPersonaje");

                entity.HasOne(d => d.NfilmNavigation)
                    .WithMany(p => p.PersonajeFilms)
                    .HasForeignKey(d => d.Nfilm)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Film");

                entity.HasOne(d => d.NpersonajeNavigation)
                    .WithMany(p => p.PersonajeFilms)
                    .HasForeignKey(d => d.Npersonaje)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Personaje");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Nombre)
                    .HasName("PK__Usuario__75E3EFCEE49B0D86");

                entity.ToTable("Usuario");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Contraseña)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
