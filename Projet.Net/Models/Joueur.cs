using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet.Net.Models
{
    public class Joueur
    {
        [Key]
        public int JoueurId { get; set; }

        [Required]
        public string JoueurPseudo { get; set; }
        [Required]  
        public string JoueurName { get; set; }
        [Required]
        public string JoueurEmail { get; set; }

        [ForeignKey("EquipeId")]
        public int? EquipeId { get; set; }

        public virtual Equipe Equipe { get; set; }
    }
}
