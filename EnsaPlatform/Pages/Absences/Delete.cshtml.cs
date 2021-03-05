using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Absences
{
    public class DeleteModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DeleteModel(EnsaPlatform.Data.EnsaContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Absence = await _context.Absences.FindAsync(id);

            if (Absence != null)
            {
                _context.Absences.Remove(Absence);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
