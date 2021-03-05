using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Absences
{
    public class IndexModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public IndexModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public IList<Absence> Absence { get; set; }

        public async Task OnGetAsync()
        { 
            Absence = await _context.Absences
                .Include(a => a.Etudiant)
                .Include(a => a.Sceance).ToListAsync();
        }
    }
}
