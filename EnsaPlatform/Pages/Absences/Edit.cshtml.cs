using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Absences
{
    public class EditModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public EditModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Absence Absence { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Absence = await _context.Absences
                .Include(a => a.Etudiant)
                .Include(a => a.Sceance).FirstOrDefaultAsync(m => m.AbsenceID == id);

            if (Absence == null)
            {
                return NotFound();
            }
            ViewData["EtudiantID"] = new SelectList(_context.Etudiants, "EtudiantID", "EtudiantID");
            ViewData["SceanceID"] = new SelectList(_context.Sceances, "SceanceID", "SceanceID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Absence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbsenceExists(Absence.AbsenceID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AbsenceExists(int id)
        {
            return _context.Absences.Any(e => e.AbsenceID == id);
        }
    }
}
