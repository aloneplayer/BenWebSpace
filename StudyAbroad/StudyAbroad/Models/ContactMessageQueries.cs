using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StudyAbroad.Models
{
    public static class ContactMessageQueries
    {
        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ContactMessage GetMessage(this DbSet<ContactMessage> source, int id)
        {
            return source.SingleOrDefault(c => c.MessageID == id);
        }

        /// <summary>
        /// Gets the contact mesage for pagination.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static Pagination<ContactMessage> GetContactMessageForPagination(this DbSet<ContactMessage> source, int page)
        {
            int count = Configuration.TheStudyAbroadSection.Current.PageSize;
            int index = (page - 1) * count;

            var query = from c in source
                        orderby c.AddedDate descending
                        select c;

            return new Pagination<ContactMessage>(
                            query.Skip(index).Take(count),
                            index,
                            count,
                            source.Count()
                        );
        }
    }
}