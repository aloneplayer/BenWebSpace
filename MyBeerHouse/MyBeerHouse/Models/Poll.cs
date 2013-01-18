using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Table("Polls", Schema = "TheBeerHouse")]
    public class Poll
    {
        public int PollID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        [StringLength(256)]
        public string QuestionText { get; set; }
        [StringLength(256)]
        public string Path { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? ArchivedDate { get; set; }

        public virtual ICollection<PollOption> PollOptions { get; set; }
    }
}
