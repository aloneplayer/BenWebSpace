using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MyBeerHouse.Models
{
    public static class CommerceQueries
    {
        /// <summary>
        /// Gets the shipping methods.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IQueryable<ShippingMethod> GetShippingMethods(this DbSet<ShippingMethod> source)
        {
            return from sm in source
                   orderby sm.Price ascending
                   select sm;
        }

        /// <summary>
        /// Cheapests the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static ShippingMethod Cheapest(this IQueryable<ShippingMethod> source)
        {
            return source.OrderBy(sm => sm.Price).FirstOrDefault();
        }

        /// <summary>
        /// Gets the shipping method.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static ShippingMethod GetShippingMethod(this DbSet<ShippingMethod> source, int id)
        {
            return source.FirstOrDefault(sm => sm.ShippingMethodID == id);
        }

        /// <summary>
        /// Gets the department.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static Department GetDepartment(this DbSet<Department> source, int id)
        {
            return source.SingleOrDefault(d => d.DepartmentID == id);
        }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IEnumerable<Department> GetDepartments(this DbSet<Department> source)
        {
            return from d in source
                   orderby d.Title
                   select d;
        }

        /// <summary>
        /// Gets the product.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static Product GetProduct(this DbSet<Product> source, int id)
        {
            return source.SingleOrDefault(p => p.ProductID == id);
        }

        /// <summary>
        /// Gets the departments.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IQueryable<Product> GetProducts(this DbSet<Product> source)
        {
            return from p in source
                   orderby p.Title
                   select p;
        }

        /// <summary>
        /// Gets the products.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="cart">The cart.</param>
        /// <returns></returns>
        public static IQueryable<Product> GetProducts(this DbSet<Product> source, ShoppingCart cart)
        {
            var productIds = cart.Select(item => item.Product.ProductID);

            return from p in source
                   where productIds.Contains(p.ProductID)
                   orderby p.Title
                   select p;
        }

        /// <summary>
        /// Bies the department.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="departmentId">The department id.</param>
        /// <returns></returns>
        public static IQueryable<Product> ByDepartment(this IQueryable<Product> source, int departmentId)
        {
            return from p in source
                   where p.DepartmentID == departmentId
                   select p;
        }


        /// <summary>
        /// Ins the stock.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IQueryable<Product> InStock(this IQueryable<Product> source)
        {
            return from p in source
                   where p.UnitsInStock > 0
                   select p;
        }

        public static void InsertOnSubmit(this DbSet<Order> source, Order order)
        {
            if (order.OrderID == default(int))
            {
                // New entity
                source.Add(order);
            }
            else
            {
                // Existing entity
                source.Attach(order);
            }
        }

        public static void InsertOnSubmit(this DbSet<Product> source, Product product)
        {
            if (product.ProductID == default(int))
            {
                // New entity
                source.Add(product);
            }
            else
            {
                // Existing entity
                source.Attach(product);
            }
        }

        public static void InsertOnSubmit(this DbSet<ShippingMethod> source, ShippingMethod shippingMethod)
        {
            if (shippingMethod.ShippingMethodID == default(int))
            {
                // New entity
                source.Add(shippingMethod);
            }
            else
            {
                // Existing entity
                source.Attach(shippingMethod);
            }
        }

        public static void DeleteOnSubmit(this DbSet<Product> source, Product product)
        {
            source.Remove(product);
        }

        public static void DeleteOnSubmit(this DbSet<ShippingMethod> source, ShippingMethod shippingMethod)
        {
            source.Remove(shippingMethod);
        }
    }
}