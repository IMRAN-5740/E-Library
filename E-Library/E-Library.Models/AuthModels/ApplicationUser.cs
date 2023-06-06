using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Models.AuthModels
{
    public class ApplicationUser:IdentityUser
    {
        
        [Required(ErrorMessage = "Please Enter your First Name")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
       
        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Please Enter your Last Name")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter your RegNo")]
        [DisplayName("Registration No")]
        public string RegNo { get; set; }
        [Required(ErrorMessage = "Please Enter your DeptName")]
        [DisplayName("Department Name")]
        public string DeptName { get; set; }
       
        [DisplayName("Address")]
        public string? Address { get; set; }

    }
}
