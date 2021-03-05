using System.Collections.Generic;

namespace EnsaPlatform.Models
{
    public class Module
    {
        public string ModuleID { get; set; }
        public int ProfesseurID { get; set; }
        public string TITRE { get; set; }

        public ICollection<Module_Niveau> Module_Niveaus { get; set; }
        public ICollection<Module_Matiere> Module_Matieres { get; set; }

        public Professeur Professeur { get; set; }


    }
}
