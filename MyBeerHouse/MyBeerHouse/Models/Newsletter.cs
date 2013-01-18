using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Table("Newsletters", Schema = "TheBeerHouse")]
    public class Newsletter
    {
        public int NewsletterID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        [StringLength(256)]
        public string Subject { get; set; }
        public string PlainTextBody { get; set; }
        public string HtmlBody { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        public DateTime? DateSent { get; set; }
    }
}
