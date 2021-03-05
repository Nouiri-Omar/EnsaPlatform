using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Modules
{
    public class IndexModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public IndexModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public IList<Module> Module { get; set; }

        public async Task OnGetAsync()
        {
            Module = await _context.Modules
                            .Include(s => s.Professeur)
                            .ToListAsync();
        }
    }
}
