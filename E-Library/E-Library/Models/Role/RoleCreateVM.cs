using System.ComponentModel.DataAnnotations;


namespace E_Library.Models.Role

{
    public class RoleCreateVM
    {
        
        [StringLength(50)]
        [Required(ErrorMessage = "Please Provide your Role Name")]
        public string RoleName { get; set; }
        
       
    }
}
