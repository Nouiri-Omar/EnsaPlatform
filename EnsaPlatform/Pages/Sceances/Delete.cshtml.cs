using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Sceances
{
    public class DeleteModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DeleteModel(EnsaPlatform.Data.EnsaContext context)
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
                .Include(s => s.Professeur).FirstOrDefaultAsync(m => m.SceanceID == id);

            if (Sceance == null)
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

            Sceance = await _context.Sceances.FindAsync(id);

            if (Sceance != null)
            {
                _context.Sceances.Remove(Sceance);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
