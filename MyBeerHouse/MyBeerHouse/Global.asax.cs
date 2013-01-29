using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyBeerHouse
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region Articles

            //Home page
            routes.MapRoute("ArticleIndex",
                            "",
                            new
                            {
                                controller = "Article",
                                action = "Index",
                                category = (string)null,
                                page = 1
                            }
                            );

            //get published articles for a specific category and page
            routes.MapRoute(
                            "ArticleCategoryViewIndexPaged",
                            "articles/categories/{category}/page{page}",
                            new
                            {
                                controller = "Article",
                                action = "Index",
                                category = (string)null,
                                page = (int?)null
                            },
                            new { category = "[a-zA-Z0-9\\-]+", page = "[0-9]+" }
                            );

            //get published articles for a specific category for the first page:
            routes.MapRoute(
                            "ArticleCategoryViewIndex",
                            "articles/categories/{category}",
                            new
                            {
                                controller = "Article",
                                action = "Index",
                                category = (string)null,
                                page = 1
                            },
                            new { category = "[a-zA-Z0-9\\-]+", page = "[0-9]+" }
                            );

            //get published articles for a specific page for all categories:
            routes.MapRoute(
                            "ArticleIndexPaged",
                            "articles/page{page}",
                            new
                            {
                                controller = "Article",
                                action = "Index",
                                category = (string)null,
                                page = (int?)null
                            },
                            new { page = "[0-9]+" }
                            );

            routes.MapRoute("ArticleCategoryIndex",
                            "articles/categories",
                            new { controller = "Article", action = "CategoryIndex" }
                            );

            routes.MapRoute(
                            "ArticleView",
                            "articles/{id}/{*path}",
                            new { controller = "Article", action = "ViewArticle", id = (string)null, path = (string)null },
                            new { id = "[0-9]+", path = "[a-zA-Z0-9\\-]*" }
                           );
            #endregion

            #region Article - Admin
            routes.MapRoute(
                    "ArticleManage",
                    "admin/articles",
                    new { controller = "Article", action = "ManageArticles", page = 1 }
                    );

            routes.MapRoute(
                "ArticleManagePaged",
                "admin/articles/page{page}",
                new { controller = "Article", action = "ManageArticles", page = (int?)null },
                new { page = "[0-9]+" }
                );

            routes.MapRoute(
                "ArticleCategoryManage",
                "admin/articles/categories",
                new { controller = "Article", action = "ManageCategories" }
                );

            //show all the comments that can be edited on page one:
            routes.MapRoute(
                    "ArticleCommentManage",
                    "admin/articles/comments",
                    new { controller = "Article", action = "ManageComments", page = 1 }
                    );

            //show all the comments that can be edited on a certain page
            routes.MapRoute(
                    "ArticleCommentManagePaged",
                    "admin/articles/comments/page{page}",
                    new { controller = "Article", action = "ManageComments", page = (int?)null },
                    new { page = "[0-9]+" }
                    );

            routes.MapRoute(
                "ArticleCreate",
                "admin/articles/create",
                new { controller = "Article", action = "CreateArticle" }
            );

            routes.MapRoute(
                "ArticleEdit",
                "admin/articles/edit/{articleId}",
                new { controller = "Article", action = "EditArticle", articleId = (int?)null },
                new { articleId = "[0-9]+" }
            );

            routes.MapRoute(
                "ArticleRemove",
                "admin/articles/remove/{articleId}",
                new { controller = "Article", action = "RemoveArticle", articleId = (int?)null },
                new { articleId = "[0-9]+" }
                );

            routes.MapRoute(
                        "ArticleCategoryCreate",
                        "admin/articles/categories/create",
                        new { controller = "Article", action = "CreateCategory" }
                        );

            routes.MapRoute(
                         "ArticleCategoryEdit",
                         "admin/articles/categories/edit/{categoryId}",
                         new { controller = "Article", action = "EditCategory", categoryId = (int?)null },
                         new { categoryId = "[0-9]+" }
                         );

            routes.MapRoute(
                        "ArticleCategoryRemove",
                        "admin/articles/categories/remove/{categoryId}",
                        new { controller = "Article", action = "RemoveCategory", categoryId = (int?)null },
                        new { categoryId = "[0-9]+" }
                        );
            #endregion

            #region Poll
            routes.MapRoute(
                            "PollsIndex",
                            "polls",
                            new { controller = "Poll", action = "Index", page = 1 }
                        );

            routes.MapRoute(
                "PollsIndexPaged",
                "polls/page{page}",
                new { controller = "Poll", action = "Index", page = (int?)null },
                new { page = "[0-9]+" }
            );

            #region Admin

            routes.MapRoute(
                "PollCreate",
                "admin/polls/create",
                new { controller = "Poll", action = "CreatePoll" }
            );

            routes.MapRoute(
                "PollEdit",
                "admin/polls/edit/{pollId}",
                new { controller = "Poll", action = "EditPoll", pollId = (int?)null },
                new { pollId = "[0-9]+" }
            );

            routes.MapRoute(
                "PollRemove",
                "admin/polls/remove/{pollId}",
                new { controller = "Poll", action = "RemovePoll", pollId = (int?)null },
                new { pollId = "[0-9]+" }
            );

            routes.MapRoute(
                "PollManager",
                "admin/polls",
                new { controller = "Poll", action = "ManagePolls", page = 1 }
            );

            routes.MapRoute(
                "PollManagerPaged",
                "admin/polls/page{page}",
                new { controller = "Poll", action = "ManagePolls", page = (int?)null },
                new { page = "[0-9]+" }
            );

            #endregion
            #endregion

            #region Newsletter - Chapter 8

            routes.MapRoute(
                "NewsletterIndex",
                "newsletters",
                new { controller = "Newsletter", action = "Index" }
            );

            #region Admin

            routes.MapRoute(
                "NewsletterCreate",
                "admin/newsletters/create",
                new { controller = "Newsletter", action = "CreateNewsletter" }
            );

            routes.MapRoute(
                "NewsletterEdit",
                "admin/newsletters/edit/{newsletterId}",
                new { controller = "Newsletter", action = "EditNewsletter", newsletterId = (int?)null },
                new { newsletterId = "[0-9]+" }
            );

            routes.MapRoute(
                "NewsletterRemove",
                "admin/newsletters/remove/{newsletterId}",
                new { controller = "Newsletter", action = "RemoveNewsletter", newsletterId = (int?)null },
                new { newsletterId = "[0-9]+" }
            );

            routes.MapRoute(
                "NewsletterManage",
                "admin/newsletters",
                new { controller = "Newsletter", action = "ManageNewsletters" }
            );

            #endregion

            #endregion



            #region Forums - Chapter 9

            routes.MapRoute(
                "ForumPostCreate",
                "forums/{forumId}/post",
                new { controller = "Forum", action = "CreatePost", forumId = (int?)null },
                new { forumId = "[0-9]+" }
            );

            routes.MapRoute(
                "ForumPostReply",
                "forums/posts/{parentPostId}/reply",
                new { controller = "Forum", action = "CreatePost", parentPostId = (int?)null },
                new { parentPostId = "[0-9]+" }
            );

            routes.MapRoute(
                "ForumsIndex",
                "forums",
                new { controller = "Forum", action = "Index" }
            );

            routes.MapRoute(
                "Forum",
                "forums/{forumId}/{*path}",
                new { controller = "Forum", action = "ViewForum", forumId = (int?)null, path = (string)null, page = 1 },
                new { forumId = "[0-9]+" }
            );

            routes.MapRoute(
                "ForumPaged",
                "forums/{forumId}/{path}/page{page}",
                new { controller = "Forum", action = "ViewForum", forumId = (int?)null, path = (string)null, page = (int?)null },
                new { forumId = "[0-9]+" }
            );

            routes.MapRoute(
                "ForumPost",
                "forums/posts/{postId}/{*path}",
                new { controller = "Forum", action = "ViewPost", postId = (int?)null, path = (string)null, page = 1 },
                new { postId = "[0-9]+" }
            );

            routes.MapRoute(
                "ForumPostPaged",
                "forums/posts/{postId}/{path}/page{page}",
                new { controller = "Forum", action = "ViewPost", postId = (int?)null, path = (string)null, page = (int?)null },
                new { postId = "[0-9]+" }
            );

            #region Admin

            routes.MapRoute(
                "ForumCreate",
                "admin/forums/create",
                new { controller = "Forum", action = "CreateForum" }
            );

            routes.MapRoute(
                "ForumEdit",
                "admin/forums/edit/{forumId}",
                new { controller = "Forum", action = "EditForum", forumId = (int?)null },
                new { forumId = "[0-9]+" }
            );

            routes.MapRoute(
                "ForumRemove",
                "admin/forums/remove/{forumId}",
                new { controller = "Forum", action = "RemoveForum", forumId = (int?)null },
                new { forumId = "[0-9]+" }
            );

            routes.MapRoute(
                "ForumPostsManager",
                "admin/forums/posts",
                new { controller = "Forum", action = "ManagePosts" }
            );

            routes.MapRoute(
                "ForumManager",
                "admin/forums",
                new { controller = "Forum", action = "ManageForums" }
            );

            #endregion

            #endregion
            //--Default url by project template
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}