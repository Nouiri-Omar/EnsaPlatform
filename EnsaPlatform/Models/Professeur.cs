using System;
using System.Collections.Generic;

namespace EnsaPlatform.Models
{
    public class Professeur
    {
        public int ProfesseurID { get; set; }
        public int DepartementID { get; set; }
        public string CIN { get; set; }
        public string NOM { get; set; }
        public string PRENOM { get; set; }
        public string EMAIL { get; set; }
        public string TEL { get; set; }
        public DateTime DATE_NAISSANCE { get; set; }
        public string LIEU_NAISSANCE { get; set; }
        public string ADDRESSE { get; set; }
        public string CHEF_DEPARTEMENT { get; set; }


        public Departement Departement { get; set; }
        public ICollection<Niveau> Niveaux { get; set; }

        public ICollection<Sceance> Sceances { get; set; }
        public ICollection<Module> Modules { get; set; }
        public ICollection<Matiere> Matieres { get; set; }







    }
}
