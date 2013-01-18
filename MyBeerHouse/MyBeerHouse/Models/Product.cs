using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Serializable]
    [Table("Products", Schema = "TheBeerHouse")]
    public class Product
    {
        public int ProductID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public int DepartmentID { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string SKU { get; set; }
        public decimal UnitPrice { get; set; }
        public int DiscountPercentage { get; set; }
        public int UnitsInStock { get; set; }
        [StringLength(256)]
        public string SmallImageUrl { get; set; }
        [StringLength(256)]
        public string FullImageUrl { get; set; }

        public virtual Department Department { get; set; }
    }
}
