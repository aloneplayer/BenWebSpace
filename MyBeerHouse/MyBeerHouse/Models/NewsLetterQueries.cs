using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyBeerHouse.Models
{
    public static class NewsletterQueries
    {
        public static void InsertOnSubmit(this DbSet<Newsletter> source, Newsletter newsletter)
        {
            if (newsletter.NewsletterID == default(int))
            {
                // New entity
                source.Add(newsletter);
            }
            else
            {
                // Existing entity
                source.Attach(newsletter);
            }
        }

        public static void DeleteOnSubmit(this DbSet<Newsletter> source, Newsletter newsletter)
        {
            source.Remove(newsletter);
        }
    }
}