using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnsaPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EnsaPlatform.Areas.Identity.Data;


namespace EnsaPlatform.Pages.Etudiants
{

    public class StudSceancesModel : PageModel
    {
        UserManager<EnsaPlatformUser> currentuser;
        private readonly EnsaPlatform.Data.EnsaContext _context;

        public StudSceancesModel(EnsaPlatform.Data.EnsaContext context, UserManager<EnsaPlatformUser> userManager)
        {
            currentuser = userManager;
            _context = context;
        }

        public Etudiant Etudiant { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {


            if (User.IsInRole("student"))
            {
                int id = -1;
                Etudiant etudiant = await _context.Etudiants
                    .FirstOrDefaultAsync(m => m.EMAIL == currentuser.GetUserName(User));
                if (etudiant.EtudiantID > 0)
                {
                    id = etudiant.EtudiantID;
                }
                if (id < 0)
                {
                    return NotFound();
                }

                Etudiant = await _context.Etudiants
                    .Include(n => n.Niveau)
                    .ThenInclude(mo => mo.Sceances)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.EtudiantID == id);





                if (Etudiant == null)
                {
                    return NotFound();
                }
                return Page();
            }
            else {

                TempData["error"] = " the current user does not have permitssion to access this page!";
                return RedirectToPage("/Error503");
            }
        }
    }
}
