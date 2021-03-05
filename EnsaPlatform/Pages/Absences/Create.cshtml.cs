using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Absences
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
            ViewData["EtudiantID"] = new SelectList(_context.Etudiants, "EtudiantID", "EtudiantID");
            ViewData["SceanceID"] = new SelectList(_context.Sceances, "SceanceID", "SceanceID");
            return Page();
        }

        [BindProperty]
        public Absence Absence { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Absences.Add(Absence);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
