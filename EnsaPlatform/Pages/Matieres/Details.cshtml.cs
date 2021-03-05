using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Matieres
{
    public class DetailsModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DetailsModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public Matiere Matiere { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Matiere = await _context.Matieres.FirstOrDefaultAsync(m => m.MatiereID == id);

            Matiere = await _context.Matieres.FirstOrDefaultAsync(m => m.MatiereID == id);


            if (Matiere == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
