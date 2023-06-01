using E_Library.Models.EntityModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Models.LibraryBook

{
    public class BookCreateVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter your Book Name :")]
        [DisplayName("Book Name")]
        public string BookName { get; set; }
        [Required(ErrorMessage = "Please Enter your Book Name :")]
        [DisplayName("Book Code")]
        public string BookCode { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }

       // [ForeignKey("CategoryId")]
        //public Category Categories { get; set; }


        [Required]
        [Display(Name = " Author Name")]
        public int AuthorId { get; set; }

        //[ForeignKey("AuthorId")]
       // public Author Authors { get; set; }
        [Required]
        [DisplayName("Publication Name")]
        public string PublicationName { get; set; }
        [Required]
        [DisplayName("Book Picture")]
        public string Picture { get; set; }
        [DisplayName("Book Price")]
        public double? Price { get; set; }
        [Required]
        [DisplayName("Book Quantity")]
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "Book Stock Status")]
        public bool IsAvailable { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public List<SelectListItem>? Authors { get; set; }


    }
}
