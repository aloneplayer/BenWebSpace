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

            //
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

            routes.MapRoute(
                            "ArticleCategoryIndex",
                            "articles/categories",
                            new { controller = "Article", action = "CategoryIndex" },
                            new { category = "[a-zA-Z0-9\\-]+", page = "[0-9]+" }
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
            #endregion

            //--Default url by project template
            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}