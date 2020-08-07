using System;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.ViewModel.System.User
{
    public class UserRegisterRequest
    {
        [Display(Name = "Fist Name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "Date Of Birth")]
        public DateTime Dob { get; set; }
        
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}