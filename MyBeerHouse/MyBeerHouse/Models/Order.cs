using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Table("Orders", Schema = "TheBeerHouse")]
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(256)]
        public string ShippingMethod { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Shipping { get; set; }
        [StringLength(256)]
        public string ShippingFirstName { get; set; }
        [StringLength(256)]
        public string ShippingLastName { get; set; }
        [StringLength(256)]
        public string ShippingStreet { get; set; }
        [StringLength(50)]
        public string ShippingPostalCode { get; set; }
        [StringLength(256)]
        public string ShippingCity { get; set; }
        [StringLength(256)]
        public string ShippingState { get; set; }
        [StringLength(256)]
        public string ShippingCountry { get; set; }
        [StringLength(256)]
        public string CustomerEmail { get; set; }
        public DateTime? ShippingDate { get; set; }
        [StringLength(256)]
        public string TransactionID { get; set; }
        [StringLength(256)]
        public string TrackingID { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
