using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace StudyAbroad.Models
{
    public class StudyAbroadDataContext : DbContext
    {
        public DbSet<ContactMessage> ContactMessages { get; set; }

        public int ExecuteCommand(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}