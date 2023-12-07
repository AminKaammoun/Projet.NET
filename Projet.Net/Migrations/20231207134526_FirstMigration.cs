using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projet.Net.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipes",
                columns: table => new
                {
                    EquipeId = table.Column<int>(type: "int", nullable: false),
                    NomEquipe = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Equipes__DC0A3743D8DD069C", x => x.EquipeId);
                });

            migrationBuilder.CreateTable(
                name: "Tournois",
                columns: table => new
                {
                    TournoiId = table.Column<int>(type: "int", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descr = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    jeu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateDebut = table.Column<DateTime>(type: "date", nullable: true),
                    DateFin = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tournois__6536E3D97F53464F", x => x.TournoiId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4C811017D6", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Resultats",
                columns: table => new
                {
                    ResultatId = table.Column<int>(type: "int", nullable: false),
                    TournoiId = table.Column<int>(type: "int", nullable: true),
                    EquipeGagnanteId = table.Column<int>(type: "int", nullable: true),
                    EquipePerdanteId = table.Column<int>(type: "int", nullable: true),
                    ScoreGagnant = table.Column<int>(type: "int", nullable: true),
                    ScorePerdant = table.Column<int>(type: "int", nullable: true),
                    DateMatch = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Resultat__20BF3E6BA2D23FFE", x => x.ResultatId);
                    table.ForeignKey(
                        name: "FK__Resultats__Equip__72C60C4A",
                        column: x => x.EquipeGagnanteId,
                        principalTable: "Equipes",
                        principalColumn: "EquipeId");
                    table.ForeignKey(
                        name: "FK__Resultats__Equip__73BA3083",
                        column: x => x.EquipePerdanteId,
                        principalTable: "Equipes",
                        principalColumn: "EquipeId");
                    table.ForeignKey(
                        name: "FK__Resultats__Tourn__71D1E811",
                        column: x => x.TournoiId,
                        principalTable: "Tournois",
                        principalColumn: "TournoiId");
                });

            migrationBuilder.CreateTable(
                name: "Joueurs",
                columns: table => new
                {
                    JoueurId = table.Column<int>(type: "int", nullable: false),
                    Pseudonyme = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "date", nullable: true),
                    EquipeId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Joueurs__D6CEE24011AFF52A", x => x.JoueurId);
                    table.ForeignKey(
                        name: "FK__Joueurs__EquipeI__6E01572D",
                        column: x => x.EquipeId,
                        principalTable: "Equipes",
                        principalColumn: "EquipeId");
                    table.ForeignKey(
                        name: "FK__Joueurs__UserId__6EF57B66",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Joueurs_EquipeId",
                table: "Joueurs",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Joueurs_UserId",
                table: "Joueurs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Resultats_EquipeGagnanteId",
                table: "Resultats",
                column: "EquipeGagnanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Resultats_EquipePerdanteId",
                table: "Resultats",
                column: "EquipePerdanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Resultats_TournoiId",
                table: "Resultats",
                column: "TournoiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Joueurs");

            migrationBuilder.DropTable(
                name: "Resultats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Equipes");

            migrationBuilder.DropTable(
                name: "Tournois");
        }
    }
}
