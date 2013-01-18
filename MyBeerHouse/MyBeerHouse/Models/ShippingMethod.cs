using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Serializable]
    [Table("ShippingMethods", Schema = "TheBeerHouse")]
    public class ShippingMethod
    {
        public int ShippingMethodID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
