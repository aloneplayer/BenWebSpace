using System;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Table("Country", Schema = "TheBeerHouse")]
    public class Country
    {
        public Guid CountryID { get; set; }
        [StringLength(100)]
        public string CountryName { get; set; }
    }
}
