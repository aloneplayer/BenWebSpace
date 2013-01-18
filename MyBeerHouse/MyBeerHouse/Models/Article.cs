using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyBeerHouse.Models
{
    [Table("Articles", Schema = "TheBeerHouse")]
    public class Article
    {
        public int ArticleID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public int CategoryID { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        [StringLength(256)]
        public string Path { get; set; }
        [StringLength(4000)]
        public string Abstract { get; set; }
        public string Body { get; set; }
        [StringLength(256)]
        public string Country { get; set; }
        [StringLength(256)]
        public string State { get; set; }
        [StringLength(256)]
        public string City { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool Approved { get; set; }
        public bool Listed { get; set; }
        public bool CommentsEnabled { get; set; }
        /// <summary>
        /// should not be accessible by the anonymous users 
        /// (users who aren’t logged in).
        /// </summary>
        public bool OnlyForMembers { get; set; }
        public int ViewCount { get; set; }
        public int Votes { get; set; }
        public int TotalRating { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>The location.</value>
        [NotMapped]
        public string Location
        {
            get
            {
                string city = this.City ?? String.Empty;
                string state = this.State ?? String.Empty;
                string country = this.Country ?? String.Empty;

                string location = city.Split(';')[0];
                if (state.Length > 0)
                {
                    if (location.Length > 0)
                        location += ", ";
                    location += state.Split(';')[0];
                }
                if (country.Length > 0)
                {
                    if (location.Length > 0)
                        location += ", ";
                    location += country.Split(';')[0];
                }
                return location;
            }
        }


        /// <summary>
        /// Gets the average rating.
        /// </summary>
        /// <value>The average rating.</value>
        [NotMapped]
        public double AverageRating
        {
            get
            {
                if (this.Votes >= 1)
                    return ((double)this.TotalRating / (double)this.Votes);
                else
                    return 0D;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Article"/> is published.
        /// </summary>
        /// <value><c>true</c> if published; otherwise, <c>false</c>.</value>
        [NotMapped]
        public bool Published
        {
            get
            {
                return (this.Approved && this.ReleaseDate <= DateTime.Now && this.ExpireDate > DateTime.Now);
            }
        }

        /// <summary>
        /// Increments the view count.
        /// </summary>
        public void IncrementViewCount()
        {
            this.ViewCount++;
        }

        /// <summary>
        /// Rates the specified rating.
        /// </summary>
        /// <param name="rating">The rating.</param>
        /// <returns></returns>
        public void Rate(int rating)
        {
            this.Votes++;
            this.TotalRating += rating;
        }

    }
}
