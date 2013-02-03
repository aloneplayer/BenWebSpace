using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyBeerHouse.Models
{
    /// <summary>
    /// provides extension methods for Poll and PollOption
    /// </summary>
    public static class PollQueries
    {
        /// <summary>
        /// Currents the poll.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static Poll CurrentPoll(this DbSet<Poll> source)
        {
            return source.FirstOrDefault(p => p.IsCurrent == true);
        }

        /// <summary>
        /// Gets the poll.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static Poll GetPoll(this DbSet<Poll> source, int id)
        {
            return source.SingleOrDefault(p => p.PollID == id);
        }

        /// <summary>
        /// Gets the poll option.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static PollOption GetPollOption(this DbSet<PollOption> source, int id)
        {
            return source.SingleOrDefault(o => o.OptionID == id);
        }

        /// <summary>
        /// Gets the polls.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="archived">The archived.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static Pagination<Poll> GetPolls(this DbSet<Poll> source, bool? archived, int page)
        {
            int count = Configuration.TheBeerHouseSection.Current.Polls.PageSize;
            int index = (page - 1) * count;

            var query = from p in source
                        orderby p.AddedDate descending
                        where (archived == null || p.IsArchived == (archived.HasValue ? archived : false))
                        select p;

            return new Pagination<Poll>(
                query.Skip(index).Take(count),
                index,
                count,
                query.Count()
            );
        }

        public static void InsertOnSubmit(this DbSet<Poll> source, Poll poll)
        {
            if (poll.PollID == default(int))
            {
                // New entity
                source.Add(poll);
            }
            else
            {
                // Existing entity
                source.Attach(poll);
            }
        }

        public static void DeleteOnSubmit(this DbSet<Poll> source, Poll poll)
        {
            source.Remove(poll);
        }

        public static void DeleteOnSubmit(this DbSet<PollOption> source, PollOption pollOption)
        {
            source.Remove(pollOption);
        }
    }
}