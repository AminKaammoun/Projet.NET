using System.ComponentModel.DataAnnotations;

namespace Projet.Net.Models
{
    public class Tournois
    {
        [Key]
        public int tournoisId { get; set; }
        [Required]
        public string tournoisName { get; set; }
        public string tournoisDescription { get; set; }
        public string jeu { get; set; }

        public string tournoisType { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }

        public List<Equipe> Equipe { get; set; } = new List<Equipe>();
        public List<Resultat> Resultat { get; set; } = new List<Resultat>();
    }
}
