using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Notes
{
    public class IndexModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public IndexModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public IList<Note> Note { get; set; }

        public async Task OnGetAsync()
        {
            Note = await _context.Notes
                .Include(n => n.Etudiant)
                .Include(n => n.Matiere).ToListAsync();
        }
    }
}
