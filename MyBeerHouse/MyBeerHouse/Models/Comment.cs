using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MyBeerHouse.Models
{
    [Table("Comments", Schema = "TheBeerHouse")]
    public class Comment
    {
        public int CommentID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public string AddedByEmail { get; set; }
        public string AddedByIP { get; set; }
        public int ArticleID { get; set; }
        public string Body { get; set; }

        public virtual Article Article { get; set; }

        /// <summary>
        /// Gets the encoded body.
        /// </summary>
        /// <value>The encoded body.</value>
        [NotMapped]
        public string EncodedBody
        {
            get
            {
                return HttpContext.Current.Server.HtmlEncode(this.Body);
            }
        }
    }
}
