using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Sceances
{
    public class DetailsModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DetailsModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

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
    }
}
