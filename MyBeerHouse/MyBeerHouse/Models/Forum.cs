using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Table("Forums", Schema = "TheBeerHouse")]
    public class Forum
    {
        public int ForumID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        [StringLength(256)]
        public string Path { get; set; }
        public bool Moderated { get; set; }
        public int Importance { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(256)]
        public string ImageUrl { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
