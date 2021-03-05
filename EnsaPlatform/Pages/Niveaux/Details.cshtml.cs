using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Niveaux
{
    public class DetailsModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DetailsModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

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
    }
}
