namespace EnsaPlatform.Models
{
    public class Note // association
    {
        public int NoteID { get; set; }
        public int EtudiantID { get; set; }
        public string MatiereID { get; set; }
        public float NoteMatiere { get; set; }

        public Etudiant Etudiant { get; set; }
        public Matiere Matiere { get; set; }

    }
}
