using E_Library.Models.EntityModels;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Library.Models.LibraryBook

{
    public class BookDeleteVM
    {

        public int Id { get; set; }
       
        [DisplayName("Book Name")]
        public string BookName { get; set; }
       
        [DisplayName("Book Code")]
        public string BookCode { get; set; }
       
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Categories { get; set; }
        
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Authors { get; set; }
        [DisplayName("Publication Name ")]
        public string PublicationName { get; set; }
        [DisplayName("Book Picture")]
        public string Picture { get; set; }
        [DisplayName("Book Price")]
        public double? Price { get; set; }
        [DisplayName("Book Quantity")]
        public int Quantity { get; set; }
        [DisplayName("Book Stock Status")]
        public bool IsAvailable { get; set; }


    }
}
