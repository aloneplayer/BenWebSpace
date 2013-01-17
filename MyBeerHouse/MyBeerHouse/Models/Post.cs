using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Linq;
using System.Web.Security;

namespace MyBeerHouse.Models
{
    [Table("Posts", Schema = "TheBeerHouse")]
    public class Post
    {
        public int PostID { get; set; }
        public DateTime AddedDate { get; set; }
        public string AddedBy { get; set; }
        public string AddedByIP { get; set; }
        public int ForumID { get; set; }
        public int? ParentPostID { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        [StringLength(256)]
        public string Path { get; set; }
        public string Body { get; set; }
        public bool Approved { get; set; }
        public bool Closed { get; set; }
        public int VoteCount { get; set; }
        public int ViewCount { get; set; }
        public int ReplyCount { get; set; }
        [StringLength(256)]
        public string LastPostBy { get; set; }
        public DateTime LastPostDate { get; set; }

        public virtual Forum Forum { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        [NotMapped]
        public short UserVote
        {
            get
            {
                var user = HttpContext.Current.User;

                if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
                    return (short)0;

                
                return Votes.GetVoteDirection(user.Identity.Name);
            }
        }

        /// <summary>
        /// 
        /// 
        /// Get the avatar for the AddedBy user.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public string GetAddedByAvatarUrl(int size)
        {
            MembershipUser membershipUser = Membership.GetUser(AddedBy, false);
            string identity = AddedByIP;

            if (membershipUser != null && membershipUser.Email != null)
                identity = membershipUser.Email.ToLower();


            return String.Format("http://www.gravatar.com/avatar/{0}?s={1}&d=identicon",
                identity.ToHashString("MD5"),  // Extension method in  ManagedFusion.dll
                size);
        }

        /// <summary>
        /// Get the avatar for the LastPostBy user.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public string GetLastPostByAvatarUrl(int size)
        {
            MembershipUser membershipUser = Membership.GetUser(LastPostBy, false);
            string identity = AddedByIP;

            if (membershipUser != null && membershipUser.Email != null)
                identity = membershipUser.Email.ToLower();

            return String.Format("http://www.gravatar.com/avatar/{0}?s={1}&d=identicon", identity.ToHashString("MD5"), size);
        }
    }
}
