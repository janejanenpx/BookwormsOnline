using BookwormsOnline_231660A.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookwormsOnline_231660A.Pages
{
    public class LogoutModel : PageModel
    {
		private readonly SignInManager<ApplicationUser> signInManager;
		public LogoutModel(SignInManager<ApplicationUser> signInManager)
		{
			this.signInManager = signInManager;
		}

		public void OnGet()
        {
        }

		public async Task<IActionResult> OnPostLogoutAsync()
		{
			await signInManager.SignOutAsync();
			//         HttpContext.Session.Clear();

			return RedirectToPage("Login");
		}
		public async Task<IActionResult> OnPostDontLogoutAsync()
		{
			return RedirectToPage("Index");
		}

	}
}
