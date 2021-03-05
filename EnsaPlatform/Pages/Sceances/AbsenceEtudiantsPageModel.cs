using EnsaPlatform.Data;
using EnsaPlatform.Models;
using EnsaPlatform.Models.EnsaViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnsaPlatform.Pages.Absences
{
    public class AbsenceEtudiantsPageModel
    {
        public List<AbsenceEtudiants> AbsencesList;


        /*
         pere : sceance
        fils : eleves
         */



        public void PopulateAbsencesList(EnsaContext context, Sceance sceance)
        {
            var Etudiants = from m in context.Etudiants
                            select m;

            var allEtudiants = Etudiants.Where(s => s.NiveauID == sceance.NiveauID);

            var SceanceAbsence = new HashSet<int>(
                sceance.Absences.Select(c => c.EtudiantID));
            AbsencesList = new List<AbsenceEtudiants>();
            foreach (var eleve in allEtudiants)
            {
                AbsencesList.Add(new AbsenceEtudiants
                {
                    EtudiantID = eleve.EtudiantID,
                    NOM = eleve.NOM,
                    PRENOM = eleve.PRENOM,
                    Assigned = SceanceAbsence.Contains(eleve.EtudiantID)
                });
            }
        }

        public void UpdateAbsencesList(EnsaContext context, string[] selectedEtudiant, Sceance sceanceToUpadet)
        {


            if (selectedEtudiant == null)
            {
                sceanceToUpadet.Absences = new List<Absence>();
                return;
            }

            var selectedEtudiantHS = new HashSet<string>(selectedEtudiant);
            var SceanceAbsence = new HashSet<int>(
                sceanceToUpadet.Absences.Select(c => c.EtudiantID));

            var Etudiants = from m in context.Etudiants
                            select m;
            var allEtudiants = Etudiants.Where(s => s.NiveauID == sceanceToUpadet.NiveauID);

            Console.WriteLine("===================== I was here");

            foreach (var eleve in allEtudiants)
            {
                if (selectedEtudiantHS.Contains(eleve.EtudiantID.ToString()))
                {
                    if (!SceanceAbsence.Contains(eleve.EtudiantID))
                    {
                        sceanceToUpadet.Absences.Add(
                            new Absence
                            {
                                SceanceID = sceanceToUpadet.SceanceID,
                                EtudiantID = eleve.EtudiantID,
                                Present = 0
                            });
                    }
                }
                else
                {
                    if (SceanceAbsence.Contains(eleve.EtudiantID))
                    {
                        Absence EtudiantToRemove = sceanceToUpadet
                                .Absences
                                .SingleOrDefault(i => i.EtudiantID == eleve.EtudiantID);
                        context.Remove(EtudiantToRemove);
                    }
                }
            }

        }
    }
}
