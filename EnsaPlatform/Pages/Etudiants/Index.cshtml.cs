using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Etudiants
{
    public class IndexModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public IndexModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public IList<Etudiant> Etudiant { get; set; }

        public async Task OnGetAsync()
        {
            Etudiant = await _context.Etudiants
                .Include(e => e.Niveau).ToListAsync();
        }
    }
}
