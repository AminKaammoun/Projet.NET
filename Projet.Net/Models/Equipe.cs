namespace Projet.Net.Models
{
    public class Equipe
    {
        public int EquipeId { get; set; }  
        public string EquipeName { get; set; }
        public List<Joueur> Joueurs { get; set; }
        public List<Tournois> Tournois { get; set; }
    }
}
