using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBeerHouse.Models
{
    public interface IPagination
    {
        /// <summary>
        /// Gets the current page number.
        /// </summary>
        /// <value>The page number.</value>
        int PageNumber { get; }

        /// <summary>
        /// Gets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        int PageSize { get; }

        /// <summary>
        /// Gets the total page count.
        /// </summary>
        /// <value>The total page count.</value>
        int PageCount { get; }

        /// <summary>
        /// Gets the start index of the item.
        /// </summary>
        /// <value>The start index of the item.</value>
        int StartIndex { get; }

        /// <summary>
        /// Gets the total item count.
        /// </summary>
        /// <value>The total item count.</value>
        int TotalCount { get; }
    }
}