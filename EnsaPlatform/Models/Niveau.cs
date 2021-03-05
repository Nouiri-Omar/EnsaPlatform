using System.Collections.Generic;

namespace EnsaPlatform.Models
{
    public class Niveau
    {

        public string NiveauID { get; set; }
        public int ProfesseurID { get; set; }
        public string TITRE { get; set; }
        public string FILIERE { get; set; }

        public ICollection<Etudiant> Etudiants { get; set; }
        public ICollection<Sceance> Sceances { get; set; }
        public ICollection<Module_Niveau> Module_Niveaus { get; set; }
        public Professeur Professeur { get; set; }



    }
}
