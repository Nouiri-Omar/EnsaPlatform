using EnsaPlatform.Data;
using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Niveaux
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

            var niveau = new Niveau();
            niveau.Module_Niveaus = new List<Module_Niveau>();

            // Provides an empty collection for the foreach loop
            // foreach (var course in Model.AssignedCourseDataList)
            // in the Create Razor page.
            PopulateNiveauHasModules(_context, niveau);

            return Page();
        }

        private void PopulateNiveauHasModules(EnsaContext context, Niveau niveau)
        {
            NiveauModulePageModel m = new NiveauModulePageModel();
            m.PopulateNiveauHasModules(context, niveau);
            ViewData["module"] = m.NiveauHasModulesList;
        }

        [BindProperty]
        public Niveau Niveau { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedModule)
        {
            var newNiveau = new Niveau();
            if (selectedModule != null)
            {
                newNiveau.Module_Niveaus = new List<Module_Niveau>();
                foreach (var module in selectedModule)
                {
                    var moduleToAdd = new Module_Niveau
                    {
                        ModuleID = module
                    };
                    newNiveau.Module_Niveaus.Add(moduleToAdd);
                }
            }

            if (await TryUpdateModelAsync<Niveau>(
                newNiveau,
                "Niveau",
                i => i.NiveauID,
                i => i.TITRE,
                i => i.ProfesseurID,
                i => i.FILIERE
                ))
            {
                _context.Niveaux.Add(newNiveau);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateNiveauHasModules(_context, newNiveau);
            return Page();

        }
    }
}
