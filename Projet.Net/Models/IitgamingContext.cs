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
        => optionsBuilder.UseSqlServer("Server=DESKTOP-735IGRG;Database=IITGaming;Integrated Security=True;Trust Server Certificate=yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipe>(entity =>
        {
            entity.HasKey(e => e.EquipeId).HasName("PK__Equipes__DC0A3743E753EDCB");

            entity.Property(e => e.NomEquipe).HasMaxLength(255);
        });

        modelBuilder.Entity<Joueur>(entity =>
        {
            entity.HasKey(e => e.JoueurId).HasName("PK__Joueurs__D6CEE24039261FB7");

            entity.Property(e => e.DateNaissance).HasColumnType("date");
            entity.Property(e => e.Pseudonyme).HasMaxLength(255);

            entity.HasOne(d => d.Equipe).WithMany(p => p.Joueurs)
                .HasForeignKey(d => d.EquipeId)
                .HasConstraintName("FK__Joueurs__EquipeI__5441852A");

            entity.HasOne(d => d.User).WithMany(p => p.Joueurs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Joueurs__UserId__5535A963");
        });

        modelBuilder.Entity<Resultat>(entity =>
        {
            entity.HasKey(e => e.ResultatId).HasName("PK__Resultat__20BF3E6B71488BF9");

            entity.Property(e => e.DateMatch).HasColumnType("date");

            entity.HasOne(d => d.EquipeGagnante).WithMany(p => p.ResultatEquipeGagnantes)
                .HasForeignKey(d => d.EquipeGagnanteId)
                .HasConstraintName("FK__Resultats__Equip__59063A47");

            entity.HasOne(d => d.EquipePerdante).WithMany(p => p.ResultatEquipePerdantes)
                .HasForeignKey(d => d.EquipePerdanteId)
                .HasConstraintName("FK__Resultats__Equip__59FA5E80");

            entity.HasOne(d => d.Tournoi).WithMany(p => p.Resultats)
                .HasForeignKey(d => d.TournoiId)
                .HasConstraintName("FK__Resultats__Tourn__5812160E");
        });

        modelBuilder.Entity<Tournoi>(entity =>
        {
            entity.HasKey(e => e.TournoiId).HasName("PK__Tournois__6536E3D9319CC133");

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
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CC3E4DF30");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
