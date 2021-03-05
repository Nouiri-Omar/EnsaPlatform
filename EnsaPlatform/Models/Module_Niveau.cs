namespace EnsaPlatform.Models
{
    public class Module_Niveau
    {
        public int Module_NiveauID { get; set; }
        public string ModuleID { get; set; }
        public string NiveauID { get; set; }

        public Niveau Niveau { get; set; }
        public Module Module { get; set; }
    }
}
