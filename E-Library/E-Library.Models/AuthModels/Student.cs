using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Models.AuthModels
{
    public class Student:IdentityUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter your Name")]
        [DisplayName("Student Name")]
        public string SName { get; set; }
        [Required(ErrorMessage = "Please Enter your Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter your RegNo")]
        [DisplayName("Registration No")]
        public string RegNo { get; set; }
        [Required(ErrorMessage = "Please Enter your DeptName")]
        [DisplayName("Department Name")]
        public string DeptName { get; set; }
        [Required(ErrorMessage = "Please Enter your Phone No")]
        [DisplayName("PhoneNo")]
        public string PhoneNumber { get; set; }
        [DisplayName("Address")]
        public string? Address { get; set; }


    }
}
