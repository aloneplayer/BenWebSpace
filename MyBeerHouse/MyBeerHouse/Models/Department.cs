using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Serializable]
    [Table("Departments", Schema = "TheBeerHouse")]
    public class Department
    {
        public int DepartmentID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        public int Importance { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(256)]
        public string ImageUrl { get; set; }
    }
}
