using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Professeurs
{
    public class DeleteModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DeleteModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Professeur Professeur { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Professeur = await _context.Professeurs
                .Include(p => p.Departement).FirstOrDefaultAsync(m => m.ProfesseurID == id);

            if (Professeur == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Professeur = await _context.Professeurs.FindAsync(id);

            if (Professeur != null)
            {
                _context.Professeurs.Remove(Professeur);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
