using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Professeurs
{
    public class DetailsModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DetailsModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public Professeur Professeur { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Professeur = await _context.Professeurs
                .Include(p => p.Departement)
                .Include(s => s.Matieres)
                .FirstOrDefaultAsync(m => m.ProfesseurID == id);

            if (Professeur == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
