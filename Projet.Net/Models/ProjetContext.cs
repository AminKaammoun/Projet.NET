using Microsoft.EntityFrameworkCore;

namespace Projet.Net.Models
{
    public class ProjetContext
    {
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Tournois> Tournois { get; set; }
        public DbSet<Resultat> Resultats { get; set; }
        public DbSet<Joueur> Joueurs { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipe>()
                .HasMany(e => e.Tournois)
                .WithMany(t => t.Equipe)
                .UsingEntity(j => j.ToTable("EquipeTournoi"));

           
        }
    }
}
