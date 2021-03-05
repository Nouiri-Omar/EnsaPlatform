using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Matieres
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

            ViewData["Mod"] = _context.Administrations.ToList();

            //= _context.Modules.Select(a =>
            //        new SelectListItem
            //        {
            //            Value = a.ModuleID.ToString(),
            //            Text = a.TITRE
            //        }).ToList();

            return Page();
        }

        [BindProperty]
        public Matiere Matiere { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            _context.Matieres.Add(Matiere);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
