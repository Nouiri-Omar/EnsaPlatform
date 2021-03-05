using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EnsaPlatform.Pages.Administrations
{
    public class EditModel : PageModel
    {
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public EditModel(EnsaPlatform.Data.EnsaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Administration Administration { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Administration = await _context.Administrations.FirstOrDefaultAsync(m => m.AdministrationID == id);

            if (Administration == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Administration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministrationExists(Administration.AdministrationID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AdministrationExists(int id)
        {
            return _context.Administrations.Any(e => e.AdministrationID == id);
        }
    }
}
