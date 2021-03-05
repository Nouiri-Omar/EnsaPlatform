using EnsaPlatform.Data;
using EnsaPlatform.Models;
using EnsaPlatform.Models.EnsaViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace EnsaPlatform.Pages.Modules
{
    public class ModuleMatierePageModel : PageModel
    {

        public List<ModuleHasMatiers> ModuleHasMatiersList;

        public void PopulateModuleHasMatiers(EnsaContext context, Module module)
        {
            var allMatiere = context.Matieres;
            var ModMat = new HashSet<string>(module.Module_Matieres.Select(c => c.MatiereID));
            ModuleHasMatiersList = new List<ModuleHasMatiers>();
            foreach (var matiere in allMatiere)
            {
                ModuleHasMatiersList.Add(new ModuleHasMatiers
                {
                    MatiereID = matiere.MatiereID,
                    TITRE = matiere.TITRE,
                    Assigned = ModMat.Contains(matiere.MatiereID)

                }); ;
            }
        }

        public void UpdateModuleMatiere(EnsaContext context,
            string[] selectedMatiere, Module moduleToUpdate)
        {
            if (selectedMatiere == null)
            {
                moduleToUpdate.Module_Matieres = new List<Module_Matiere>();
                return;
            }

            var selectedMatiereHS = new HashSet<string>(selectedMatiere);
            var ModuleMatieres = new HashSet<string>
                (moduleToUpdate.Module_Matieres.Select(c => c.Matiere.MatiereID));

            foreach (var matiere in context.Matieres)
            {
                if (selectedMatiereHS.Contains(matiere.MatiereID.ToString()))
                {
                    if (!ModuleMatieres.Contains(matiere.MatiereID))
                    {
                        moduleToUpdate.Module_Matieres.Add(
                            new Module_Matiere
                            {
                                ModuleID = moduleToUpdate.ModuleID,
                                MatiereID = matiere.MatiereID
                            });
                    }
                }
                else
                {
                    if (ModuleMatieres.Contains(matiere.MatiereID))
                    {
                        Module_Matiere matiereToRemove
                            = moduleToUpdate
                                .Module_Matieres
                                .SingleOrDefault(i => i.MatiereID == matiere.MatiereID);
                        context.Remove(matiereToRemove);
                    }
                }
            }
        }
    }
}

