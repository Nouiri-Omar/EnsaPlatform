using EnsaPlatform.Data;
using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Niveaux
{
    public class EditModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public EditModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Niveau Niveau { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //Niveau = await _context.Niveaux.FirstOrDefaultAsync(m => m.NiveauID == id);

            //if (Niveau == null)
            //{
            //    return NotFound();
            //}
            //return Page();
            ViewData["Prof"] = _context.Professeurs.Select(a =>
                  new SelectListItem
                  {
                      Value = a.ProfesseurID.ToString(),
                      Text = a.NOM
                  }).ToList();
            if (id == null)
            {
                return NotFound();
            }

            Niveau = await _context.Niveaux
                .Include(s => s.Module_Niveaus).ThenInclude(i => i.Module)
                .FirstOrDefaultAsync(m => m.NiveauID == id);

            if (Niveau == null)
            {
                return NotFound();
            }

            PopulateNiveauHasModules(_context, Niveau);
            return Page();
        }


        private void PopulateNiveauHasModules(EnsaContext context, Niveau niveau)
        {
            NiveauModulePageModel m = new NiveauModulePageModel();
            m.PopulateNiveauHasModules(context, niveau);
            ViewData["module"] = m.NiveauHasModulesList;
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string id, string[] selectedModule)
        {

            if (id == null)
            {
                return NotFound();
            }

            var niveauToUpdate = await _context.Niveaux
                    .Include(i => i.Module_Niveaus)
                        .ThenInclude(i => i.Module)
                     .FirstOrDefaultAsync(s => s.NiveauID == id);

            if (niveauToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Niveau>(
                niveauToUpdate,
                "Niveau",
                i => i.NiveauID,
                i => i.ProfesseurID,
                i => i.TITRE,
                i => i.FILIERE))
            {
                //if (
                //    String.IsNullOrWhiteSpace(sceanceToUpdate.Professeur?.ProfesseurID.ToString()) ||
                //    String.IsNullOrWhiteSpace(sceanceToUpdate.Matiere?.MatiereID) ||
                //    String.IsNullOrWhiteSpace(sceanceToUpdate.Niveau?.NiveauID))
                //{
                //    sceanceToUpdate.Absences = null;
                //}
                UpdateNiveauHasModules(_context, selectedModule, niveauToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateNiveauHasModules(_context, selectedModule, niveauToUpdate);
            PopulateNiveauHasModules(_context, niveauToUpdate);
            return Page();
        }

        private void UpdateNiveauHasModules(EnsaContext context, string[] selectedModule, Niveau niveauToUpdate)
        {
            NiveauModulePageModel m = new NiveauModulePageModel();
            m.UpdateNiveauHasModules(context, selectedModule, niveauToUpdate);
        }
    }

}
