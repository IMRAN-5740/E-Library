using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace E_Library.Models.EntityModels
{
    public class IssuesBook
    {
        public int Id { get; set; }
        [Display(Name = "Issue")]
        public int IssueId { get; set; }

        [Display(Name = "Book")]
        public int BookId { get; set; }

        [ForeignKey("IssueId")]
        public Issue Issues { get; set; }

        [ForeignKey("BookId")]
        public Category Books { get; set; }
    }
}
