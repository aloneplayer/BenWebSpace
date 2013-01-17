using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Table("Votes", Schema = "TheBeerHouse")]
    public class Vote
    {
        [Key, Column(Order = 0)]
        public int PostID { get; set; }
        [Key, Column(Order = 1)]
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedByIP { get; set; }
        public short Direction { get; set; }

        public virtual Post Post { get; set; }
    }
}
