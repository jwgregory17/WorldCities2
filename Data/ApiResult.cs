using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace WorldCities2.Data
{
    public class ApiResult<T>
    {
        /// <summary>
        /// Private constructor called by the CreateAsync method.
        /// </summary>
        private ApiResult(
            List<T> data,
            int count,
            int pageIndex,
            int pageSize,
            string sortColumn,
            string sortOrder)
        {
            Data = data;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SortColumn = sortColumn;
            SortOrder = sortOrder;
        }

        #region Methods
        /// <summary>
        /// Pages and/or sorts a IQueryable source.
        /// </summary>
        /// <param name="source">An IQueryable source of generic 
        /// type</param>
        /// <param name="pageIndex">Zero-based current page index 
        /// (0 = first page)</param>
        /// <param name="pageSize">The actual size of each 
        /// page</param>
        /// <param name="sortColumn">The sorting column name</param>
        /// <param name="sortOrder">The sorting order ("ASC" or 
        /// "DESC")</param>
        /// <returns>
        /// A object containing the IQueryable paged/sorted result 
        /// and all the relevant paging/sorting navigation info.
        /// </returns>
        public static async Task<ApiResult<T>> CreateAsync(
            IQueryable<T> source,
            int pageIndex,
            int pageSize,
            string sortColumn = null,
            string sortOrder = null)
        {
            var count = await source.CountAsync();

            if (!String.IsNullOrEmpty(sortColumn)
                && IsValidProperty(sortColumn))
            {
                sortOrder = !String.IsNullOrEmpty(sortOrder)
                            && sortOrder.ToUpper() == "ASC"
                    ? "ASC"
                    : "DESC";
                source = source.OrderBy(
                    String.Format(
                        "{0} {1}",
                        sortColumn,
                        sortOrder)
                );
            }

            var data = await source
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ApiResult<T>(
                data,
                count,
                pageIndex,
                pageSize,
                sortColumn,
                sortOrder);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Checks if the given property name exists
        /// to protect against SQL injection attacks
        /// </summary>
        public static bool IsValidProperty(
            string propertyName,
            bool throwExceptionIfNotFound = true)
        {
            var prop = typeof(T).GetProperty(
                propertyName,
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.Instance);
            if (prop == null && throwExceptionIfNotFound)
                throw new NotSupportedException(
                    String.Format(
                        "ERROR: Property '{0}' does not exist.",
                        propertyName)
                );
            return prop != null;
        }
        #endregion

        #region Properties
        // The data result.
        public List<T> Data { get; private set; }

        // Zero-based index of current page.
        public int PageIndex { get; private set; }

        /// <summary>
        /// Number of items contained in each page.
        /// </summary>
        public int PageSize { get; private set; }

        // Total items count
        public int TotalCount { get; private set; }

        // Total pages count
        public int TotalPages { get; private set; }

        // Sorting Column name (or null if none set)
        public string SortColumn { get; set; }

        // Sorting Order ("ASC", "DESC" or null if none set)
        public string SortOrder { get; set; }

        // TRUE if the current page has a previous page, 
        // FALSE otherwise.
        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }

        // TRUE if the current page has a next page, FALSE otherwise.
        public bool HasNextPage
        {
            get
            {
                return ((PageIndex + 1) < TotalPages);
            }
        }
        #endregion
    }
}
