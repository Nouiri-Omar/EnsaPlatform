using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Modules
{
    public class DeleteModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DeleteModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Module Module { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Module = await _context.Modules.FirstOrDefaultAsync(m => m.ModuleID == id);

            if (Module == null)
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

            Module module = await _context.Modules
                .Include(i => i.Module_Matieres)
                .SingleAsync(i => i.ModuleID == id);

            if (module == null)
            {
                return RedirectToPage("./Index");
            }

            _context.Modules.Remove(module);

            await _context.SaveChangesAsync();

            Module = await _context.Modules.FindAsync(id);

            if (Module != null)
            {
                _context.Modules.Remove(Module);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
