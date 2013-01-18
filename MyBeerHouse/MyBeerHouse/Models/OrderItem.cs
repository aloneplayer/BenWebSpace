using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Table("OrderItems", Schema = "TheBeerHouse")]
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        [StringLength(256)]
        public string SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
