using System;
using System.Collections.Generic;

namespace EnsaPlatform.Models
{
    public class Sceance // association
    {
        public int SceanceID { get; set; }
        public string NiveauID { get; set; }
        public string MatiereID { get; set; }
        public int ProfesseurID { get; set; }
        public DateTime DATE_HEURE { get; set; }

        public ICollection<Absence> Absences { get; set; }

        public Matiere Matiere { get; set; }

        public Professeur Professeur { get; set; }
        public Niveau Niveau { get; set; }



    }
}
