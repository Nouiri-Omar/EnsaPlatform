using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Professeurs
{
    public class IndexModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public IndexModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public IList<Professeur> Professeur { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (User.IsInRole("Professeur")) { return RedirectToPage("/index"); }
            Professeur = await _context.Professeurs
                .Include(p => p.Departement).ToListAsync();
            return Page();
        }
    }
}
