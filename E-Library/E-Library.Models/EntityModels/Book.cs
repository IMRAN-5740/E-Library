using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Library.Models.EntityModels
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Book Name")]
        public string BookName { get; set; }
        [Required]
        [DisplayName("Book Code")]
        public string BookCode { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Categories { get; set; }


        [Required]
        [Display(Name = " Author Name")]
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")] 
        public Author Authors { get; set; }


        [Required] 
        [DisplayName("Publication Name")]
        public string PublicationName { get; set; }
        [Required] 
        [DisplayName("Book Picture")] 
        public string Picture { get; set; }
        [DisplayName("Price")]
        public double? Price { get; set; }
        [Required] 
        [DisplayName("Quantity")] 
        public int Quantity { get; set; }
        [Required] 
        [Display(Name = "Book Status")] 
        public bool IsAvailable { get; set; }



    }
}
