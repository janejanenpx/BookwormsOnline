using BookwormsOnline_231660A.Model;
using BookwormsOnline_231660A.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static BookwormsOnline_231660A.Model.AuthDbContext;

namespace BookwormsOnline_231660A.Pages
{
    public class LoginModel : PageModel
    {

		[BindProperty]
		public Login LModel { get; set; }

		private readonly SignInManager<ApplicationUser> signInManager;
        //private UserManager<ApplicationUser> userManager;
        //private readonly AuthDbContext _context;

        public LoginModel(SignInManager<ApplicationUser> signInManager)
		{
			this.signInManager = signInManager;
            //this.userManager = userManager;
            //_context = context;
        }

		public void OnGet()
        {
        }

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
                //// Add before login attempt
                //var auditEntry = new AuditLog
                //{
                //    UserId = (await userManager.FindByEmailAsync(LModel.Email))?.Id,
                //    Activity = "Login Attempt",
                //    Timestamp = DateTime.UtcNow,
                //    IpAddress = HttpContext.Connection.RemoteIpAddress?.ToString()
                //};

                //_context.AuditLogs.Add(auditEntry);
                //await _context.SaveChangesAsync();

                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email,
				LModel.Password, LModel.RememberMe, false);
				if (identityResult.Succeeded)
				{
                    //HttpContext.Session.SetString("SessionStart", DateTime.Now.ToString());
                    return RedirectToPage("Index");
				}
				ModelState.AddModelError("", "Username or Password incorrect");
			}
			return Page();
		}
	}
}
