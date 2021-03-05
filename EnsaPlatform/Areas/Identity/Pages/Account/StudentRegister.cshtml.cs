using EnsaPlatform.Areas.Identity.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EnsaPlatform.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class StudentRegisterModel : PageModel
    {
        private readonly SignInManager<EnsaPlatformUser> _signInManager;
        private readonly UserManager<EnsaPlatformUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EnsaPlatformContext _ensa;
        private readonly ILogger<RegisterModel> _logger;
        private readonly EnsaPlatform.Data.EnsaContext _context;
        private readonly IEmailSender _emailSender;

        public StudentRegisterModel(
            UserManager<EnsaPlatformUser> userManager,
            SignInManager<EnsaPlatformUser> signInManager,
            ILogger<RegisterModel> logger,
            RoleManager<IdentityRole> roleManager,
            EnsaPlatformContext ensa,
            EnsaPlatform.Data.EnsaContext context,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _ensa = ensa;
            _context = context;

        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            public string Name { get; set; }
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ViewData["Stud"] = _context.Etudiants.Select(a =>
 new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
 {
     Value = a.EMAIL,
     Text = a.EMAIL
 }).ToList();
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new EnsaPlatformUser { UserName = Input.Email, Email = Input.Email, Name = Input.Name };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Professeur"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Professeur"));
                    }
                    if (!await _roleManager.RoleExistsAsync("user_handler"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("user_handler"));
                    }
                    if (!await _roleManager.RoleExistsAsync("student"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("student"));
                    }
                    await _userManager.AddToRoleAsync(user, "student");
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                       // await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
