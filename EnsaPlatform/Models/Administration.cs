using System;

namespace EnsaPlatform.Models
{
    public class Administration
    {
        public int AdministrationID { get; set; }
        public string CIN { get; set; }
        public string NOM { get; set; }
        public string PRENOM { get; set; }
        public string EMAIL { get; set; }
        public string TEL { get; set; }
        public DateTime DATE_NAISSANCE { get; set; }
        public string LIEU_NAISSANCE { get; set; }
        public string ADDRESSE { get; set; }
        public string Occupation { get; set; }
    }
}
