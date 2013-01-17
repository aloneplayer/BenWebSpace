using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Configuration;

namespace MyBeerHouse.Models
{
    public static class ForumQueries
    {
        public static short GetVoteDirection(this ICollection<Vote> source, string userName)
        {
            var query = from v in source
                        where v.AddedBy == userName
                        select v.Direction;

            return query.SingleOrDefault();
        }

        /// <summary>
        /// Gets the vote.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="postId">The post id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static Vote GetVote(this DbSet<Vote> source, int postId, string userName)
        {
            var query = from v in source
                        where v.PostID == postId && v.AddedBy == userName
                        select v;

            return query.SingleOrDefault();
        }

        /// <summary>
        /// Gets the post.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="postId">The post id.</param>
        /// <returns></returns>
        public static Post GetPost(this DbSet<Post> source, int postId)
        {
            return source.SingleOrDefault(p => p.PostID == postId);
        }

        /// <summary>
        /// Gets the forum.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="forumId">The forum id.</param>
        /// <returns></returns>
        public static Forum GetForum(this DbSet<Forum> source, int forumId)
        {
            return source.SingleOrDefault(f => f.ForumID == forumId);
        }

        /// <summary>
        /// Gets the posts.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="forumId">The forum id.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static Pagination<Post> GetPosts(this DbSet<Post> source, int forumId, int page)
        {
            int count = Configuration.TheBeerHouseSection.Current.Forums.PostsPageSize;
            int index = (page - 1) * count;

            var query = from p in source
                        where p.ParentPostID == null && p.ForumID == forumId && p.Approved
                        orderby p.LastPostDate descending
                        select p;

            return new Pagination<Post>(
                query.Skip(index).Take(count),
                index,
                count,
                query.Count()
            );
        }

        /// <summary>
        /// Gets the forums.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IEnumerable<Forum> GetForums(this DbSet<Forum> source)
        {
            return source.OrderBy(f => f.Importance);
        }

        /// <summary>
        /// Gets the replies.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="postId">The post id.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static Pagination<Post> GetReplies(this DbSet<Post> source, int postId, int page)
        {
            int count = Configuration.TheBeerHouseSection.Current.Forums.ThreadsPageSize;
            int index = (page - 1) * count;

            var query = from p in source
                        where p.ParentPostID == postId && p.Approved
                        orderby p.AddedDate
                        select p;

            return new Pagination<Post>(
                query.Skip(index).Take(count),
                index,
                count,
                query.Count()
            );
        }

        public static void InsertOnSubmit(this DbSet<Forum> source, Forum forum)
        {
            if (forum.ForumID == default(int))
            {
                // New entity
                source.Add(forum);
            }
            else
            {
                // Existing entity
                source.Attach(forum);
            }
        }

        public static void InsertOnSubmit(this DbSet<Post> source, Post post)
        {
            if (post.PostID == default(int))
            {
                // New entity
                source.Add(post);
            }
            else
            {
                // Existing entity
                source.Attach(post);
            }
        }

        public static void DeleteOnSubmit(this DbSet<Forum> source, Forum forum)
        {
            source.Remove(forum);
        }

        public static void DeleteOnSubmit(this DbSet<Post> source, Post post)
        {
            source.Remove(post);
        }
    }
}