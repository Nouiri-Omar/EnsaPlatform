using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Matieres
{
    public class IndexModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public IndexModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public IList<Matiere> Matiere { get; set; }

        public async Task OnGetAsync()
        {
            Matiere = await _context.Matieres
                .Include(s => s.Professeur)
                            .ToListAsync();
        }
    }
}
