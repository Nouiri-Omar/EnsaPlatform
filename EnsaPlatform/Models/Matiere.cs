using System.Collections.Generic;

namespace EnsaPlatform.Models
{
    public class Matiere
    {
        public string MatiereID { get; set; }
        public int ProfesseurID { get; set; }
        public string TITRE { get; set; }

        public ICollection<Sceance> Sceances { get; set; }
        public ICollection<Module> Module_Matieres { get; set; }
        public ICollection<Note> Notes { get; set; }

        public Professeur Professeur { get; set; }

    }
}
