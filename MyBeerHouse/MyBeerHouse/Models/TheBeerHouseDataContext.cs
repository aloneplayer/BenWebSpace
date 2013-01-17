using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace MyBeerHouse.Models
{
    public class TheBeerHouseDataContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Forum> Forums { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasMany(x => x.Posts)
                .WithOptional()
                .HasForeignKey(x => x.ParentPostID);
        }

        public void SubmitChanges()
        {
            SaveChanges();
        }

        public int ExecuteCommand(string sql, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sql, parameters);
        }
    }
}