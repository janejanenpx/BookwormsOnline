using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookwormsOnline_231660A.Model
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [PersonalData]
        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [PersonalData]
        [Required, Phone]
        public string MobileNumber { get; set; }

        [PersonalData]
        [Required]
        public string EncryptedCreditCard { get; set; }

        [PersonalData]
        [Required, MaxLength(200)]
        public string BillingAddress { get; set; }

        [PersonalData]
        [Required, MaxLength(200)]
        public string ShippingAddress { get; set; }

        [PersonalData]
        public string PhotoPath { get; set; }

        //public string SessionToken { get; set; }
        //public DateTime LastLogin { get; set; }
    }
}
