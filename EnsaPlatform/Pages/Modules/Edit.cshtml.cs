using EnsaPlatform.Data;
using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Modules
{
    public class EditModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public EditModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Module Module { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Module = await _context.Modules
                .Include(s => s.Module_Matieres).ThenInclude(i => i.Matiere)
                .FirstOrDefaultAsync(m => m.ModuleID == id);

            if (Module == null)
            {
                return NotFound();
            }
            ViewData["Prof"] = _context.Professeurs.Select(a =>
                new SelectListItem
                {
                    Value = a.ProfesseurID.ToString(),
                    Text = a.NOM
                }).ToList();

            PopulateModuleHasMatiers(_context, Module);
            return Page();
        }
        private void PopulateModuleHasMatiers(EnsaContext context, Module module)
        {
            ModuleMatierePageModel m = new ModuleMatierePageModel();
            m.PopulateModuleHasMatiers(context, module);
            ViewData["matiere"] = m.ModuleHasMatiersList;
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string id, string[] selectedMatiere)
        {

            if (id == null)
            {
                return NotFound();
            }

            var moduleToUpdate = await _context.Modules
                    .Include(i => i.Module_Matieres)
                        .ThenInclude(i => i.Matiere)
                     .FirstOrDefaultAsync(s => s.ModuleID == id);

            if (moduleToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Module>(
                moduleToUpdate,
                "Module",
                i => i.ModuleID,
                i => i.ProfesseurID, i => i.TITRE))
            {
                //if (
                //    String.IsNullOrWhiteSpace(sceanceToUpdate.Professeur?.ProfesseurID.ToString()) ||
                //    String.IsNullOrWhiteSpace(sceanceToUpdate.Matiere?.MatiereID) ||
                //    String.IsNullOrWhiteSpace(sceanceToUpdate.Niveau?.NiveauID))
                //{
                //    sceanceToUpdate.Absences = null;
                //}
                UpdateModuleMatiere(_context, selectedMatiere, moduleToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateModuleMatiere(_context, selectedMatiere, moduleToUpdate);
            PopulateModuleHasMatiers(_context, moduleToUpdate);
            return Page();
        }

        private void UpdateModuleMatiere(EnsaContext context, string[] selectedMatieres, Module moduleToUpdate)
        {
            ModuleMatierePageModel m = new ModuleMatierePageModel();
            m.UpdateModuleMatiere(context, selectedMatieres, moduleToUpdate);
        }

        private bool ModuleExists(string id)
        {
            return _context.Modules.Any(e => e.ModuleID == id);
        }
    }
}
