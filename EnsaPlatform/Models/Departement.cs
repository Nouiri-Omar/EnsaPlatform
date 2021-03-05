using System.Collections.Generic;

namespace EnsaPlatform.Models
{
    public class Departement
    {
        public int DepartementID { get; set; }
        public string TITRE { get; set; }

        public ICollection<Professeur> Professeurs { get; set; }
    }
}
