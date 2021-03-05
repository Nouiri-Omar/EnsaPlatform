using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Matieres
{
    public class DeleteModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DeleteModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Matiere Matiere { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Matiere = await _context.Matieres.FirstOrDefaultAsync(m => m.MatiereID == id);

            if (Matiere == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Matiere = await _context.Matieres.FindAsync(id);

            if (Matiere != null)
            {
                _context.Matieres.Remove(Matiere);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
