using BookwormsOnline_231660A.Extensions;
using System.ComponentModel.DataAnnotations;

namespace BookwormsOnline_231660A.ViewModels
{
    public class Register
    {
        [Required, Display(Name = "First Name")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }


        [Required, Display(Name = "Last Name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.CreditCard)]
        public string CreditCard { get; set; }


        [Required, Phone, Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }


        [Required, Display(Name = "Billing Address")]
        [DataType(DataType.MultilineText)]
        public string BillingAddress { get; set; }


        [Required, Display(Name = "Shipping Address")]
        [DataType(DataType.MultilineText)]
        public string ShippingAddress { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{12,}$",
            ErrorMessage = "Password must be at least 12 characters with uppercase, lowercase, number, and special character")]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }


        [Required, AllowedExtensions(new[] { ".jpg", ".jpeg" })]
        public IFormFile Photo { get; set; }

        public string? RecaptchaToken { get; set; }
    }
}
