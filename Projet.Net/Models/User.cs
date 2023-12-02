using System;
using System.Collections.Generic;

namespace Projet.Net.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Joueur> Joueurs { get; set; } = new List<Joueur>();
}
