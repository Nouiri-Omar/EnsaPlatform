using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Departements
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
            return Page();
        }

        [BindProperty]
        public Departement Departement { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Departements.Add(Departement);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
