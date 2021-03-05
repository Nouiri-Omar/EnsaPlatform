using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Professeurs
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
            //ViewData["DepartementID"] = new SelectList(_context.Departements, "DepartementID", "DepartementID");
            //ViewData["DepartementID"] = new SelectList(_context.Departements, "DepartementID", "DepartementID");
            ViewData["Dep"] = _context.Departements.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.DepartementID.ToString(),
                                      Text = a.TITRE
                                  }).ToList();

            return Page();
        }

        [BindProperty]
        public Professeur Professeur { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Professeurs.Add(Professeur);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
