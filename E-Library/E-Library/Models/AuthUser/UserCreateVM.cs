using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Library.Models.AuthUser
{
    public class UserCreateVM
    {
       
        [Required(ErrorMessage ="Please Enter UserName")]
        [DisplayName("UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter your First Name")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Please Enter your Last Name")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter your RegNo")]
        [DisplayName("Reg No")]
        public string RegNo { get; set; }
        [Required(ErrorMessage = "Please Enter your DeptName")]
        [DisplayName("Dept Name")]
        public string DeptName { get; set; }
        [Required(ErrorMessage = "Please Enter your PhoneNumber")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please Enter your Valid Email")]
        [DisplayName("Email Address")]
        [StringLength(25, ErrorMessage = "Must be between 5 and 25 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }


        [DisplayName("Address")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; }



        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
