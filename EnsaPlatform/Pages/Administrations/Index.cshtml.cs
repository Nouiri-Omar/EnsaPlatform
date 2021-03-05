using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Administrations
{
    public class IndexModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public IndexModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public IList<Administration> Administration { get; set; }

        public async Task OnGetAsync()
        {
            Administration = await _context.Administrations.ToListAsync();
        }
    }
}
