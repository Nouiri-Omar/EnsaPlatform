namespace EnsaPlatform.Models
{
    public class Absence
    {
        public int AbsenceID { get; set; }
        public int SceanceID { get; set; }
        public int EtudiantID { get; set; }
        public int Present { get; set; }

        public Sceance Sceance { get; set; }
        public Etudiant Etudiant { get; set; }



    }
}
