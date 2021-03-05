using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Modules
{
    public class DetailsModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DetailsModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public Module Module { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Module = await _context.Modules
                .Include(i => i.Module_Matieres)
                    .ThenInclude(i => i.Matiere)
                .FirstOrDefaultAsync(m => m.ModuleID == id);

            if (Module == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
