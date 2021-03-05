using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Sceances
{
    public class CreateModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public CreateModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["Prof"] = _context.Professeurs.Select(a =>
                        new SelectListItem
                        {
                            Value = a.ProfesseurID.ToString(),
                            Text = a.NOM
                        }).ToList();
            ViewData["MatiereID"] = new SelectList(_context.Matieres, "MatiereID", "MatiereID");
            ViewData["NiveauID"] = new SelectList(_context.Niveaux, "NiveauID", "NiveauID");
            //ViewData["ProfesseurID"] = new SelectList(_context.Professeurs, "ProfesseurID", "ProfesseurID");
            return Page();
        }

        [BindProperty]
        public Sceance Sceance { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Sceances.Add(Sceance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
