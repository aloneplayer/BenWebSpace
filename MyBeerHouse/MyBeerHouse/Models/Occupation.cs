using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Table("Occupation", Schema = "TheBeerHouse")]
    public class Occupation
    {
        public Guid OccupationID { get; set; }
        [StringLength(100)]
        public string OccupationName { get; set; }
    }
}
