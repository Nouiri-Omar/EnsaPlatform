using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Departements
{
    public class DeleteModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public DeleteModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Departement Departement { get; set; }
        public string ErrorMessage { get; set; }
        // docs.microsoft.com/en-us/aspnet/core/data/ef-rp/crud?view=aspnetcore-5.0

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Departement = await _context.Departements
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.DepartementID == id);

            if (Departement == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Departement = await _context.Departements.FindAsync(id);


            if (Departement == null)
            {
                return NotFound();
            }

            try
            {
                _context.Departements.Remove(Departement);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}
