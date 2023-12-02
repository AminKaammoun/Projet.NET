using System;
using System.Collections.Generic;

namespace Projet.Net.Models;

public partial class Joueur
{
    public int JoueurId { get; set; }

    public string Pseudonyme { get; set; } = null!;

    public DateTime? DateNaissance { get; set; }

    public int? EquipeId { get; set; }

    public int? UserId { get; set; }

    public virtual Equipe? Equipe { get; set; }

    public virtual User? User { get; set; }
}
