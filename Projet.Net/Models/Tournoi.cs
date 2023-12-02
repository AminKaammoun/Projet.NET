using System;
using System.Collections.Generic;

namespace Projet.Net.Models;

public partial class Tournoi
{
    public int TournoiId { get; set; }

    public string Nom { get; set; } = null!;

    public string Descr { get; set; } = null!;

    public string Jeu { get; set; } = null!;

    public DateTime? DateDebut { get; set; }

    public DateTime? DateFin { get; set; }

    public virtual ICollection<Resultat> Resultats { get; set; } = new List<Resultat>();
}
