using BookwormsOnline_231660A.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookwormsOnline_231660A.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly UserManager<ApplicationUser> _userManager;
        //ADDED
        //private readonly IDataProtector _protector;

        //ADDED
        //public ApplicationUser CurrentUser { get; set; }
        //public string DecryptedCreditCard { get; set; }

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        //public IndexModel(UserManager<ApplicationUser> userManager,
        //            IDataProtectionProvider protector)
        //{
        //    _userManager = userManager;
        //    _protector = protector.CreateProtector("MySecretKey");
        //}

        //public async Task OnGet()
        //{
        //    CurrentUser = await _userManager.GetUserAsync(User);
        //    if (CurrentUser != null)
        //    {
        //        DecryptedCreditCard = _protector.Unprotect(CurrentUser.EncryptedCreditCard);
        //    }
        //}


        public void OnGet()
        {

        }
    }
}
