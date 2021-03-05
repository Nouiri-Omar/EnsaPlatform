namespace EnsaPlatform.Models
{
    public class Module_Matiere
    {
        public int Module_MatiereID { get; set; }
        public string ModuleID { get; set; }
        public string MatiereID { get; set; }

        public Matiere Matiere { get; set; }
        public Module Module { get; set; }


    }
}
