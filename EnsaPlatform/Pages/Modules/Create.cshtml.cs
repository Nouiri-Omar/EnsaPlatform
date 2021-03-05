using EnsaPlatform.Data;
using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Modules
{
    public class CreateModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public CreateModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }



        public IActionResult OnGet()
        {
            ViewData["Prof"] = _context.Professeurs.Select(a =>
            new SelectListItem
            {
                Value = a.ProfesseurID.ToString(),
                Text = a.NOM
            }).ToList();

            var module = new Module();
            module.Module_Matieres = new List<Module_Matiere>();

            // Provides an empty collection for the foreach loop
            // foreach (var course in Model.AssignedCourseDataList)
            // in the Create Razor page.
            PopulateModuleHasMatiers(_context, module);
            return Page();
        }

        private void PopulateModuleHasMatiers(EnsaContext context, Module module)
        {
            ModuleMatierePageModel m = new ModuleMatierePageModel();
            m.PopulateModuleHasMatiers(context, module);
            ViewData["matiere"] = m.ModuleHasMatiersList;
        }

        [BindProperty]
        public Module Module { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedMatiere)
        {

            var newModule = new Module();
            if (selectedMatiere != null)
            {
                newModule.Module_Matieres = new List<Module_Matiere>();
                foreach (var matiere in selectedMatiere)
                {
                    var matiereToAdd = new Module_Matiere
                    {
                        MatiereID = matiere
                    };
                    newModule.Module_Matieres.Add(matiereToAdd);
                }
            }

            if (await TryUpdateModelAsync<Module>(
                newModule,
                "Module",
                i => i.ModuleID,
                i => i.TITRE,
                i => i.ProfesseurID
                ))
            {
                _context.Modules.Add(newModule);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateModuleHasMatiers(_context, newModule);
            return Page();
        }
    }
}
