using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Etudiants
{
    public class EditModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public EditModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Etudiant Etudiant { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Etudiant = await _context.Etudiants
                .Include(e => e.Niveau).FirstOrDefaultAsync(m => m.EtudiantID == id);

            if (Etudiant == null)
            {
                return NotFound();
            }
            ViewData["NiveauID"] = new SelectList(_context.Niveaux, "NiveauID", "NiveauID");
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

            _context.Attach(Etudiant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtudiantExists(Etudiant.EtudiantID))
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

        private bool EtudiantExists(int id)
        {
            return _context.Etudiants.Any(e => e.EtudiantID == id);
        }
    }
}
