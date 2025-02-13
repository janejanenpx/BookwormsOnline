using BookwormsOnline_231660A.Model;
using BookwormsOnline_231660A.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookwormsOnline_231660A.Pages
{
    public class RegisterModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }
        private readonly IWebHostEnvironment _env;
        private readonly IDataProtectionProvider _protector;



        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IWebHostEnvironment env,
        IDataProtectionProvider protector)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _env = env;
            //_protector = dataProtectionProvider.CreateProtector("MySecretKey");
            _protector = protector;
        }

        public void OnGet()
        {
        }

        //Save data into the database
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                //var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                var protector = _protector.CreateProtector("MySecretKey");


                // Verify reCAPTCHA
                if (!await VerifyRecaptcha(RModel.RecaptchaToken))
                {
                    ModelState.AddModelError(string.Empty, "Captcha validation failed");
                    return Page();
                }

                var user = new ApplicationUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email,
                    FirstName = RModel.FirstName,
                    LastName = RModel.LastName,
                    //EncryptedCreditCard = EncryptionUtility.Encrypt(RModel.CreditCard),
                    EncryptedCreditCard = protector.Protect(RModel.CreditCard),

                    MobileNumber = RModel.MobileNumber,
                    BillingAddress = RModel.BillingAddress,
                    ShippingAddress = RModel.ShippingAddress

                };

                // Handle photo upload
                if (RModel.Photo != null)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

                    // Ensure uploads directory exists
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + RModel.Photo.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await RModel.Photo.CopyToAsync(fileStream);
                    }
                    user.PhotoPath = "/uploads/" + uniqueFileName;
                }

                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",
                error.Description);
                }
            }
            return Page();
        }



        //CAPTCHA
        private async Task<bool> VerifyRecaptcha(string token)
        {
            using var client = new HttpClient();
            var response = await client.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6LeTjdQqAAAAANSYXULft-VCpf6o3s0FCnDr0ieh&response={token}", null);
            var result = await response.Content.ReadFromJsonAsync<RecaptchaResponse>();
            return result?.Success ?? false;
        }

        private class RecaptchaResponse { public bool Success { get; set; } }

    }
}
