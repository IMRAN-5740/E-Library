namespace E_Library.Models.AuthUser
{
    public class UserIndexVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string RegNo { get; set; }
        public string DeptName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset? LockoutEnd { get;set; }
        public string Email { get; set; }
        public string? Address { get; set; }
    }
}
