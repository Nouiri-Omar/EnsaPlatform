using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Administrations
{
    public class DeleteModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DeleteModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Administration Administration { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Administration = await _context.Administrations.FirstOrDefaultAsync(m => m.AdministrationID == id);

            if (Administration == null)
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

            Administration = await _context.Administrations.FindAsync(id);

            if (Administration != null)
            {
                _context.Administrations.Remove(Administration);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
