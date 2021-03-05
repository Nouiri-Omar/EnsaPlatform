using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Notes
{
    public class DeleteModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DeleteModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Note Note { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Note = await _context.Notes
                .Include(n => n.Etudiant)
                .Include(n => n.Matiere).FirstOrDefaultAsync(m => m.NoteID == id);

            if (Note == null)
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

            Note = await _context.Notes.FindAsync(id);

            if (Note != null)
            {
                _context.Notes.Remove(Note);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
