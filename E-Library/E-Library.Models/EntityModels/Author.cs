using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace E_Library.Models.EntityModels
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Author Name")]
        public string AuthorName { get; set; }
    }
}
