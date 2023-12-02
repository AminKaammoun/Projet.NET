using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet.Net.Models
{
    public class Resultat
    {
        [Key]
        public int ResultatId { get; set; }
        
        [ForeignKey("tournoisId")]
        public int TournoiId { get; set; }
        public virtual Tournois Tournois { get; set; }
        [ForeignKey("equipeId")]
        public int EquipeGagnanteId { get; set; }
        public virtual Equipe Gagnante { get; set; }
        [ForeignKey("equipeId")]
        public int EquipePerdanteId { get; set; }
        public virtual Equipe Pertante { get; set; }
        public int ScoreGagnant { get; set; }
        public int ScorePerdant { get; set; }

    }
}
