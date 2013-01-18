using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Table("PollOptions", Schema = "TheBeerHouse")]
    public class PollOption
    {
        [Key]
        public int OptionID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public int PollID { get; set; }
        [StringLength(256)]
        public string OptionText { get; set; }
        public int Votes { get; set; }

        public virtual Poll Poll { get; set; }
    }
}
