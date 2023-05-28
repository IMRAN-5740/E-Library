using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace E_Library.Models.EntityModels
{
    public class Issue
    {
        public int Id { get; set; }
        public string IssueNo { get; set; }
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
        public DateTime IssueDate { get; set; }
        public virtual List<IssuesBook> IssuesBooks { get; set; }

    }
}
