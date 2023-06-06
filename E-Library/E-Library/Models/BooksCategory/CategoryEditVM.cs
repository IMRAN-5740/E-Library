using System.ComponentModel.DataAnnotations;

namespace E_Library.Models.BooksCategory

{
    public class CategoryEditVM
    {
        public int Id { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Please Provide your Category Name")]
        public string CategoryName { get; set; }
       
    }
}
