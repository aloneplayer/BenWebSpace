using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Table("Language", Schema = "TheBeerHouse")]
    public class Language
    {
        public Guid LanguageID { get; set; }
        [StringLength(100)]
        public string LanguageName { get; set; }
    }
}
