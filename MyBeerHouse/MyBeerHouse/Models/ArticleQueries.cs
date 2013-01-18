using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyBeerHouse.Models
{
    /// <summary>
    /// This data queries are for chapter 6 of the book.
    /// </summary>
    public static class ArticleQueries
    {
        /// <summary>
        /// Gets the article.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static Article GetArticle(this DbSet<Article> source, int id)
        {
            return source.SingleOrDefault(a => a.ArticleID == id);
        }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static Category GetCategory(this DbSet<Category> source, int id)
        {
            return source.SingleOrDefault(c => c.CategoryID == id);
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static Category GetCategory(this DbSet<Category> source, string path)
        {
            return source.SingleOrDefault(c => c.Path == path);
        }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static Comment GetComment(this DbSet<Comment> source, int id)
        {
            return source.SingleOrDefault(c => c.CommentID == id);
        }


        /// <summary>
        /// Checks to see if the specified Category exists.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static bool Exists(this DbSet<Category> source, int id)
        {
            return source.Count(c => c.CategoryID == id) > 0;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IEnumerable<Category> GetCategories(this DbSet<Category> source)
        {
            return from c in source
                   orderby c.Importance, c.Title
                   select c;
        }

        /// <summary>
        /// Gets the published articles by category.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        private static IQueryable<Article> GetPublishedArticles(this DbSet<Article> source, string category)
        {
            // make sure that category evaluates to null incase it comes in as an empty string
            category = String.IsNullOrEmpty(category) ? null : category;

            bool isAuthenticated = false;
            HttpContext context = HttpContext.Current;

            if (context.User != null && context.User.Identity != null)
                isAuthenticated = context.User.Identity.IsAuthenticated;

            var query = from a in source
                        orderby a.ReleaseDate descending
                        where a.Approved == true
                            && a.Listed == true
                            && (isAuthenticated == true || a.OnlyForMembers == false)
                            && a.ReleaseDate <= DateTime.Now
                            && (a.ExpireDate == null || a.ExpireDate > DateTime.Now)
                            && (category == null || a.Category.Path == category)
                        select a;

            return query;
        }

        /// <summary>
        /// Gets the published articles by category.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        private static IQueryable<Article> GetArticles(this DbSet<Article> source, string category)
        {
            // make sure that category always evaluates to null incase it comes in as an empty string
            category = String.IsNullOrEmpty(category) ? null : category;

            var query = from a in source
                        orderby a.ReleaseDate descending
                        where category == null || a.Category.Path == category
                        select a;

            return query;
        }

        /// <summary>
        /// Gets the articles.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static Pagination<Article> GetPublishedArticles(this DbSet<Article> source, int page)
        {
            int count = Configuration.TheBeerHouseSection.Current.Articles.PageSize;
            int index = (page - 1) * count;

            return new Pagination<Article>(
                new ArticleCollectionWrapper(source.GetPublishedArticles(null).Skip(index).Take(count)),
                index,
                count,
                GetPublishedArticlesCount(source)
            );
        }

        /// <summary>
        /// Gets the published articles by category.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="categoryId">The category id.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static Pagination<Article> GetPublishedArticles(this DbSet<Article> source, string category, int page)
        {
            int count = Configuration.TheBeerHouseSection.Current.Articles.PageSize;
            int index = (page - 1) * count;

            return new Pagination<Article>(
                new ArticleCollectionWrapper(source.GetPublishedArticles(category).Skip(index).Take(count)),
                index,
                count,
                GetPublishedArticlesCount(source, category)
            );
        }

        /// <summary>
        /// Gets the articles count.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static int GetPublishedArticlesCount(this DbSet<Article> source)
        {
            return source.GetPublishedArticles(null).Count();
        }

        /// <summary>
        /// Gets the published articles by category count.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        public static int GetPublishedArticlesCount(this DbSet<Article> source, string category)
        {
            return source.GetPublishedArticles(category).Count();
        }

        /// <summary>
        /// Gets the articles.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static Pagination<Article> GetArticles(this DbSet<Article> source, int page)
        {
            int count = Configuration.TheBeerHouseSection.Current.Articles.PageSize;
            int index = (page - 1) * count;

            return new Pagination<Article>(
                new ArticleCollectionWrapper(
                    source.GetArticles(
                        null
                        ).Skip(index).Take(count)),
                index,
                count,
                GetArticlesCount(source)
            );
        }

        /// <summary>
        /// Gets the published articles by category.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="categoryId">The category id.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static Pagination<Article> GetArticles(this DbSet<Article> source, string category, int page)
        {
            int count = Configuration.TheBeerHouseSection.Current.Articles.PageSize;
            int index = (page - 1) * count;

            return new Pagination<Article>(
                new ArticleCollectionWrapper(source.GetArticles(category).Skip(index).Take(count)),
                index,
                count,
                GetArticlesCount(source, category)
            );
        }

        /// <summary>
        /// Gets the articles count.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static int GetArticlesCount(this DbSet<Article> source)
        {
            return source.GetArticles(null).Count();
        }

        /// <summary>
        /// Gets the published articles by category count.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="categoryId">The category id.</param>
        /// <returns></returns>
        public static int GetArticlesCount(this DbSet<Article> source, string category)
        {
            return source.GetArticles(category).Count();
        }

        /// <summary>
        /// Gets the comments for moderation.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public static Pagination<Comment> GetCommentsForModeration(this DbSet<Comment> source, int page)
        {
            int count = Configuration.TheBeerHouseSection.Current.Articles.PageSize;
            int index = (page - 1) * count;

            var query = from c in source
                        orderby c.AddedDate descending
                        select c;

            return new Pagination<Comment>(
                query.Skip(index).Take(count),
                index,
                count,
                source.Count()
            );
        }

        public static void InsertOnSubmit(this DbSet<Article> source, Article article)
        {
            if (article.ArticleID == default(int))
            {
                // New entity
                source.Add(article);
            }
            else
            {
                // Existing entity
                source.Attach(article);
            }
        }

        public static void InsertOnSubmit(this DbSet<Category> source, Category category)
        {
            if (category.CategoryID == default(int))
            {
                // New entity
                source.Add(category);
            }
            else
            {
                // Existing entity
                source.Attach(category);
            }
        }

        public static void InsertOnSubmit(this DbSet<Comment> source, Comment comment)
        {
            if (comment.CommentID == default(int))
            {
                // New entity
                source.Add(comment);
            }
            else
            {
                // Existing entity
                source.Attach(comment);
            }
        }

        public static void DeleteOnSubmit(this DbSet<Article> source, Article article)
        {
            source.Remove(article);
        }

        public static void DeleteOnSubmit(this DbSet<Category> source, Category category)
        {
            source.Remove(category);
        }

        public static void DeleteOnSubmit(this DbSet<Comment> source, Comment comment)
        {
            source.Remove(comment);
        }
    }
}