using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Projet.Net.Models;

public partial class IitgamingContext : DbContext
{
    public IitgamingContext()
    {
    }

    public IitgamingContext(DbContextOptions<IitgamingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipe> Equipes { get; set; }

    public virtual DbSet<Joueur> Joueurs { get; set; }

    public virtual DbSet<Resultat> Resultats { get; set; }

    public virtual DbSet<Tournoi> Tournois { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=IITGaming;Integrated Security=True;Trust Server Certificate=yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipe>(entity =>
        {
            entity.HasKey(e => e.EquipeId).HasName("PK__Equipes__DC0A3743D8DD069C");

            entity.Property(e => e.EquipeId).ValueGeneratedNever();
            entity.Property(e => e.NomEquipe).HasMaxLength(255);
        });

        modelBuilder.Entity<Joueur>(entity =>
        {
            entity.HasKey(e => e.JoueurId).HasName("PK__Joueurs__D6CEE24011AFF52A");

            entity.Property(e => e.JoueurId).ValueGeneratedNever();
            entity.Property(e => e.DateNaissance).HasColumnType("date");
            entity.Property(e => e.Pseudonyme).HasMaxLength(255);

            entity.HasOne(d => d.Equipe).WithMany(p => p.Joueurs)
                .HasForeignKey(d => d.EquipeId)
                .HasConstraintName("FK__Joueurs__EquipeI__6E01572D");

            entity.HasOne(d => d.User).WithMany(p => p.Joueurs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Joueurs__UserId__6EF57B66");
        });

        modelBuilder.Entity<Resultat>(entity =>
        {
            entity.HasKey(e => e.ResultatId).HasName("PK__Resultat__20BF3E6BA2D23FFE");

            entity.Property(e => e.ResultatId).ValueGeneratedNever();
            entity.Property(e => e.DateMatch).HasColumnType("date");

            entity.HasOne(d => d.EquipeGagnante).WithMany(p => p.ResultatEquipeGagnantes)
                .HasForeignKey(d => d.EquipeGagnanteId)
                .HasConstraintName("FK__Resultats__Equip__72C60C4A");

            entity.HasOne(d => d.EquipePerdante).WithMany(p => p.ResultatEquipePerdantes)
                .HasForeignKey(d => d.EquipePerdanteId)
                .HasConstraintName("FK__Resultats__Equip__73BA3083");

            entity.HasOne(d => d.Tournoi).WithMany(p => p.Resultats)
                .HasForeignKey(d => d.TournoiId)
                .HasConstraintName("FK__Resultats__Tourn__71D1E811");
        });

        modelBuilder.Entity<Tournoi>(entity =>
        {
            entity.HasKey(e => e.TournoiId).HasName("PK__Tournois__6536E3D97F53464F");

            entity.Property(e => e.TournoiId).ValueGeneratedNever();
            entity.Property(e => e.DateDebut).HasColumnType("date");
            entity.Property(e => e.DateFin).HasColumnType("date");
            entity.Property(e => e.Descr).HasMaxLength(255);
            entity.Property(e => e.Jeu)
                .HasMaxLength(255)
                .HasColumnName("jeu");
            entity.Property(e => e.Nom).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C811017D6");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
