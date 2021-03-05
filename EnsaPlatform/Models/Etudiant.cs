using System;
using System.Collections.Generic;

namespace EnsaPlatform.Models
{
    public class Etudiant
    {
        public int EtudiantID { get; set; }
        public string NiveauID { get; set; }
        public string NOM { get; set; }
        public string PRENOM { get; set; }
        public string CIN { get; set; }
        public DateTime DATE_NAISSANCE { get; set; }
        public string LIEU_NAISSANCE { get; set; }
        public string EMAIL { get; set; }
        public string TEL { get; set; }
        public string ADDRESSE { get; set; }
        public int DELEGUE { get; set; }

        public ICollection<Note> Notes { get; set; }
        public ICollection<Absence> Absences { get; set; }


        public Niveau Niveau { get; set; }

    }
}
