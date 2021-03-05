using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Sceances
{
    public class IndexModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public IndexModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public IList<Sceance> Sceance { get; set; }

        public async Task OnGetAsync()
        {
            Sceance = await _context.Sceances
                .Include(s => s.Matiere)
                .Include(s => s.Niveau)
                .Include(s => s.Professeur).ToListAsync();
        }
    }
}
