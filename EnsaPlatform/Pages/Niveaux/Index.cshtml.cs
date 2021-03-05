using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Niveaux
{
    public class IndexModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public IndexModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public IList<Niveau> Niveau { get; set; }

        public async Task<Microsoft.AspNetCore.Mvc.RedirectToPageResult> OnGetAsync()
        {
            if (User.IsInRole("Admin"))
            {
                Niveau = await _context.Niveaux
                    .Include(s => s.Professeur)
                    .ToListAsync();
                return null;
            }
            else
            {
                TempData["error"] = " the current user does not have permitssion to access this page!";

                return RedirectToPage("/Error503");
            }
        }
    }
}
