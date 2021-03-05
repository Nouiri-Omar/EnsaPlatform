using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using EnsaPlatform.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnsaPlatform.Pages.Professeurs
{
    public class ProfNotesModel : PageModel
    {

        UserManager<EnsaPlatformUser> currentuser;
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public ProfNotesModel(EnsaPlatform.Data.EnsaContext context, UserManager<EnsaPlatformUser> userManager)
        {
            currentuser = userManager;
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string Niv)

        {
            if (User.IsInRole("Professeur"))
            {
                int id = -1;
                Professeur professeur = await _context.Professeurs
                    .Include(i => i.Niveaux)
                    .FirstOrDefaultAsync(m => m.EMAIL == currentuser.GetUserName(User));
                if (professeur.ProfesseurID > 0)
                {
                    id = professeur.ProfesseurID;
                }
                ViewData["EtudiantID"] = new SelectList(_context.Etudiants.Where(s => s.NiveauID == Niv), "EtudiantID", "NOM");
                ViewData["MatiereID"] = new SelectList(_context.Matieres.Where(s => s.ProfesseurID == id), "MatiereID", "TITRE");
                return Page();
            }
            else
            {
                TempData["error"] = " the current user does not have permitssion to access this page!";
                return RedirectToPage("/Error503");
            }
        }

        [BindProperty]
        public Note Note { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Notes.Add(Note);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
