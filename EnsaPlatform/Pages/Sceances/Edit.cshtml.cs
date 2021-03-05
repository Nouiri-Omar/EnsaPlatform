using EnsaPlatform.Data;
using EnsaPlatform.Models;
using EnsaPlatform.Pages.Absences;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Sceances
{
    public class EditModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public EditModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Sceance Sceance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Sceance = await _context.Sceances
                .Include(s => s.Matiere)
                .Include(s => s.Niveau)
                .Include(s => s.Professeur)
                .Include(s => s.Absences).ThenInclude(i => i.Etudiant)
                .FirstOrDefaultAsync(m => m.SceanceID == id);

            if (Sceance == null)
            {
                return NotFound();
            }
            ViewData["MatiereID"] = new SelectList(_context.Matieres, "MatiereID", "MatiereID");
            ViewData["NiveauID"] = new SelectList(_context.Niveaux, "NiveauID", "NiveauID");
            //ViewData["ProfesseurID"] = new SelectList(_context.Professeurs, "ProfesseurID", "ProfesseurID");
            ViewData["Prof"] = _context.Professeurs.Select(a =>
            new SelectListItem
            {
                Value = a.ProfesseurID.ToString(),
                Text = a.NOM
            }).ToList();
            //Sceance.Absences = new List<Absence>();

            // Provides an empty collection for the foreach loop
            // foreach (var course in Model.AssignedCourseDataList)
            // in the Create Razor page.
            PopulateAbsencesList(_context, Sceance);
            return Page();

        }

        private void PopulateAbsencesList(EnsaContext context, Sceance sceance)
        {
            AbsenceEtudiantsPageModel m = new AbsenceEtudiantsPageModel();
            m.PopulateAbsencesList(context, sceance);
            ViewData["Etudiant"] = m.AbsencesList;
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedEtudiants)
        {

            if (id == null)
            {
                return NotFound();
            }

            var sceanceToUpdate = await _context.Sceances
                .Include(i => i.Professeur)
                .Include(i => i.Matiere)
                .Include(i => i.Niveau)
                .Include(i => i.Absences)
                    .ThenInclude(i => i.Etudiant)
                .FirstOrDefaultAsync(s => s.SceanceID == id);

            if (sceanceToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Sceance>(
                sceanceToUpdate,
                "Sceance",
                i => i.NiveauID, i => i.MatiereID,
                i => i.ProfesseurID, i => i.DATE_HEURE))
            {
                //if (
                //    String.IsNullOrWhiteSpace(sceanceToUpdate.Professeur?.ProfesseurID.ToString()) ||
                //    String.IsNullOrWhiteSpace(sceanceToUpdate.Matiere?.MatiereID) ||
                //    String.IsNullOrWhiteSpace(sceanceToUpdate.Niveau?.NiveauID))
                //{
                //    sceanceToUpdate.Absences = null;
                //}
                UpdateAbsencesList(_context, selectedEtudiants, sceanceToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateAbsencesList(_context, selectedEtudiants, sceanceToUpdate);
            PopulateAbsencesList(_context, sceanceToUpdate);
            return Page();
        }

        private void UpdateAbsencesList(EnsaContext context, string[] selectedEtudiants, Sceance sceanceToUpdate)
        {
            AbsenceEtudiantsPageModel m = new AbsenceEtudiantsPageModel();
            m.UpdateAbsencesList(context, selectedEtudiants, sceanceToUpdate);
        }

        private bool SceanceExists(int id)
        {
            return _context.Sceances.Any(e => e.SceanceID == id);
        }
    }
}
