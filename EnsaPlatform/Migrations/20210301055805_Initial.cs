using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EnsaPlatform.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administration",
                columns: table => new
                {
                    AdministrationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRENOM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATE_NAISSANCE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LIEU_NAISSANCE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADDRESSE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administration", x => x.AdministrationID);
                });

            migrationBuilder.CreateTable(
                name: "Departement",
                columns: table => new
                {
                    DepartementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITRE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departement", x => x.DepartementID);
                });

            migrationBuilder.CreateTable(
                name: "Professeur",
                columns: table => new
                {
                    ProfesseurID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartementID = table.Column<int>(type: "int", nullable: false),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NOM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRENOM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATE_NAISSANCE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LIEU_NAISSANCE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADDRESSE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CHEF_DEPARTEMENT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professeur", x => x.ProfesseurID);
                    table.ForeignKey(
                        name: "FK_Professeur_Departement_DepartementID",
                        column: x => x.DepartementID,
                        principalTable: "Departement",
                        principalColumn: "DepartementID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matiere",
                columns: table => new
                {
                    MatiereID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfesseurID = table.Column<int>(type: "int", nullable: false),
                    TITRE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matiere", x => x.MatiereID);
                    table.ForeignKey(
                        name: "FK_Matiere_Professeur_ProfesseurID",
                        column: x => x.ProfesseurID,
                        principalTable: "Professeur",
                        principalColumn: "ProfesseurID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Niveau",
                columns: table => new
                {
                    NiveauID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfesseurID = table.Column<int>(type: "int", nullable: false),
                    TITRE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FILIERE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveau", x => x.NiveauID);
                    table.ForeignKey(
                        name: "FK_Niveau_Professeur_ProfesseurID",
                        column: x => x.ProfesseurID,
                        principalTable: "Professeur",
                        principalColumn: "ProfesseurID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    ModuleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfesseurID = table.Column<int>(type: "int", nullable: false),
                    TITRE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MatiereID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.ModuleID);
                    table.ForeignKey(
                        name: "FK_Module_Matiere_MatiereID",
                        column: x => x.MatiereID,
                        principalTable: "Matiere",
                        principalColumn: "MatiereID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Module_Professeur_ProfesseurID",
                        column: x => x.ProfesseurID,
                        principalTable: "Professeur",
                        principalColumn: "ProfesseurID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Etudiant",
                columns: table => new
                {
                    EtudiantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NiveauID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NOM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRENOM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATE_NAISSANCE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LIEU_NAISSANCE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TEL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ADDRESSE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DELEGUE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiant", x => x.EtudiantID);
                    table.ForeignKey(
                        name: "FK_Etudiant_Niveau_NiveauID",
                        column: x => x.NiveauID,
                        principalTable: "Niveau",
                        principalColumn: "NiveauID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sceance",
                columns: table => new
                {
                    SceanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NiveauID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MatiereID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProfesseurID = table.Column<int>(type: "int", nullable: false),
                    DATE_HEURE = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sceance", x => x.SceanceID);
                    table.ForeignKey(
                        name: "FK_Sceance_Matiere_MatiereID",
                        column: x => x.MatiereID,
                        principalTable: "Matiere",
                        principalColumn: "MatiereID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sceance_Niveau_NiveauID",
                        column: x => x.NiveauID,
                        principalTable: "Niveau",
                        principalColumn: "NiveauID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sceance_Professeur_ProfesseurID",
                        column: x => x.ProfesseurID,
                        principalTable: "Professeur",
                        principalColumn: "ProfesseurID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Module_Matieres",
                columns: table => new
                {
                    Module_MatiereID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MatiereID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module_Matieres", x => x.Module_MatiereID);
                    table.ForeignKey(
                        name: "FK_Module_Matieres_Matiere_MatiereID",
                        column: x => x.MatiereID,
                        principalTable: "Matiere",
                        principalColumn: "MatiereID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Module_Matieres_Module_ModuleID",
                        column: x => x.ModuleID,
                        principalTable: "Module",
                        principalColumn: "ModuleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Module_Niveaus",
                columns: table => new
                {
                    Module_NiveauID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NiveauID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module_Niveaus", x => x.Module_NiveauID);
                    table.ForeignKey(
                        name: "FK_Module_Niveaus_Module_ModuleID",
                        column: x => x.ModuleID,
                        principalTable: "Module",
                        principalColumn: "ModuleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Module_Niveaus_Niveau_NiveauID",
                        column: x => x.NiveauID,
                        principalTable: "Niveau",
                        principalColumn: "NiveauID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NoteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EtudiantID = table.Column<int>(type: "int", nullable: false),
                    MatiereID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    NoteMatiere = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_Note_Etudiant_EtudiantID",
                        column: x => x.EtudiantID,
                        principalTable: "Etudiant",
                        principalColumn: "EtudiantID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Note_Matiere_MatiereID",
                        column: x => x.MatiereID,
                        principalTable: "Matiere",
                        principalColumn: "MatiereID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Absence",
                columns: table => new
                {
                    AbsenceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SceanceID = table.Column<int>(type: "int", nullable: false),
                    EtudiantID = table.Column<int>(type: "int", nullable: false),
                    Present = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absence", x => x.AbsenceID);
                    table.ForeignKey(
                        name: "FK_Absence_Etudiant_EtudiantID",
                        column: x => x.EtudiantID,
                        principalTable: "Etudiant",
                        principalColumn: "EtudiantID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Absence_Sceance_SceanceID",
                        column: x => x.SceanceID,
                        principalTable: "Sceance",
                        principalColumn: "SceanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Absence_EtudiantID",
                table: "Absence",
                column: "EtudiantID");

            migrationBuilder.CreateIndex(
                name: "IX_Absence_SceanceID",
                table: "Absence",
                column: "SceanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Etudiant_NiveauID",
                table: "Etudiant",
                column: "NiveauID");

            migrationBuilder.CreateIndex(
                name: "IX_Matiere_ProfesseurID",
                table: "Matiere",
                column: "ProfesseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Module_MatiereID",
                table: "Module",
                column: "MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_Module_ProfesseurID",
                table: "Module",
                column: "ProfesseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Module_Matieres_MatiereID",
                table: "Module_Matieres",
                column: "MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_Module_Matieres_ModuleID",
                table: "Module_Matieres",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "IX_Module_Niveaus_ModuleID",
                table: "Module_Niveaus",
                column: "ModuleID");

            migrationBuilder.CreateIndex(
                name: "IX_Module_Niveaus_NiveauID",
                table: "Module_Niveaus",
                column: "NiveauID");

            migrationBuilder.CreateIndex(
                name: "IX_Niveau_ProfesseurID",
                table: "Niveau",
                column: "ProfesseurID");

            migrationBuilder.CreateIndex(
                name: "IX_Note_EtudiantID",
                table: "Note",
                column: "EtudiantID");

            migrationBuilder.CreateIndex(
                name: "IX_Note_MatiereID",
                table: "Note",
                column: "MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_Professeur_DepartementID",
                table: "Professeur",
                column: "DepartementID");

            migrationBuilder.CreateIndex(
                name: "IX_Sceance_MatiereID",
                table: "Sceance",
                column: "MatiereID");

            migrationBuilder.CreateIndex(
                name: "IX_Sceance_NiveauID",
                table: "Sceance",
                column: "NiveauID");

            migrationBuilder.CreateIndex(
                name: "IX_Sceance_ProfesseurID",
                table: "Sceance",
                column: "ProfesseurID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absence");

            migrationBuilder.DropTable(
                name: "Administration");

            migrationBuilder.DropTable(
                name: "Module_Matieres");

            migrationBuilder.DropTable(
                name: "Module_Niveaus");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "Sceance");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropTable(
                name: "Etudiant");

            migrationBuilder.DropTable(
                name: "Matiere");

            migrationBuilder.DropTable(
                name: "Niveau");

            migrationBuilder.DropTable(
                name: "Professeur");

            migrationBuilder.DropTable(
                name: "Departement");
        }
    }
}
