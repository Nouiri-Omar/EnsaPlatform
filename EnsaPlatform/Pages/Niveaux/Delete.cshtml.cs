using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Niveaux
{
    public class DeleteModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DeleteModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Niveau Niveau { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Niveau = await _context.Niveaux.FirstOrDefaultAsync(m => m.NiveauID == id);

            if (Niveau == null)
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

            Niveau = await _context.Niveaux
                .Include(s => s.Etudiants)
                .Include(s => s.Sceances)
                .Include(s => s.Module_Niveaus)
                    .ThenInclude(i => i.Module)
                .FirstOrDefaultAsync(m => m.NiveauID == id);

            if (Niveau != null)
            {
                _context.Niveaux.Remove(Niveau);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
