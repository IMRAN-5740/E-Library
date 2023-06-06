using System.ComponentModel.DataAnnotations;

namespace E_Library.Models.AuthUser
{
    public class UserEditVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string RegNo { get; set; }
        public string DeptName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? OldPassword { get; set; }
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string? NewPassword { get; set; }
    }
}
