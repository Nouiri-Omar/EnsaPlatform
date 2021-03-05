using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Administrations
{
    public class DetailsModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DetailsModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

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
    }
}
