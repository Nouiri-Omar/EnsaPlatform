using EnsaPlatform.Data;
using EnsaPlatform.Models;
using EnsaPlatform.Models.EnsaViewModels;
using System.Collections.Generic;
using System.Linq;

namespace EnsaPlatform.Pages.Niveaux
{
    public class NiveauModulePageModel
    {
        public List<NiveauHasModules> NiveauHasModulesList;

        public void PopulateNiveauHasModules(EnsaContext context, Niveau niveau)
        {
            var allModules = context.Modules;
            var ModNiv = new HashSet<string>(niveau.Module_Niveaus.Select(c => c.ModuleID));
            NiveauHasModulesList = new List<NiveauHasModules>();
            foreach (var module in allModules)
            {
                NiveauHasModulesList.Add(new NiveauHasModules
                {
                    ModuleID = module.ModuleID,
                    TITRE = module.TITRE,
                    Assigned = ModNiv.Contains(module.ModuleID)

                }); ;
            }
        }

        public void UpdateNiveauHasModules(EnsaContext context,
            string[] selectedModule, Niveau niveauToUpdate)
        {
            if (selectedModule == null)
            {
                niveauToUpdate.Module_Niveaus = new List<Module_Niveau>();
                return;
            }

            var selectedModuleHS = new HashSet<string>(selectedModule);
            var NiveauModules = new HashSet<string>
                (niveauToUpdate.Module_Niveaus.Select(c => c.Module.ModuleID));

            foreach (var module in context.Modules)
            {
                if (selectedModuleHS.Contains(module.ModuleID.ToString()))
                {
                    if (!NiveauModules.Contains(module.ModuleID))
                    {
                        niveauToUpdate.Module_Niveaus.Add(
                            new Module_Niveau
                            {
                                NiveauID = niveauToUpdate.NiveauID,
                                ModuleID = module.ModuleID
                            });
                    }
                }
                else
                {
                    if (NiveauModules.Contains(module.ModuleID))
                    {
                        Module_Niveau moduleToRemove
                            = niveauToUpdate
                                .Module_Niveaus
                                .SingleOrDefault(i => i.ModuleID == module.ModuleID);
                        context.Remove(moduleToRemove);
                    }
                }
            }
        }
    }
}
