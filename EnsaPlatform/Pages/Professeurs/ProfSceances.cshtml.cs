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


namespace EnsaPlatform.Pages.Professeurs
{
    public class ProfSceancesModel : PageModel
    {


        UserManager<EnsaPlatformUser> currentuser;
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public ProfSceancesModel(EnsaPlatform.Data.EnsaContext context, UserManager<EnsaPlatformUser> userManager)
        {
            currentuser = userManager;
            _context = context;
        }

        public ICollection<Sceance> Sceances { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

     
            if (User.IsInRole("Professeur"))
            {
                int id = -1;
                Professeur professeur = await _context.Professeurs
                    .Include(i => i.Matieres)
                    .FirstOrDefaultAsync(m => m.EMAIL == currentuser.GetUserName(User));
                if(professeur.ProfesseurID>0)
                {
                    id = professeur.ProfesseurID;
                }
                if (id < 0)
                {
                    return NotFound();
                }





                Sceances = await _context.Sceances
                    .Include(s => s.Matiere)
                    .Where(s => s.ProfesseurID == id)
                    .ToListAsync();

                if (Sceances == null)
                {
                    return NotFound();
                }
                return Page();
            }
            else
            {
                TempData["error"] = " the current user does not have permitssion to access this page!";
                return RedirectToPage("/Error503");
            }
        }
    }

    
}