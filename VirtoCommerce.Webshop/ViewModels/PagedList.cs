using System;
using System.Collections.Generic;

namespace VirtoCommerce.Webshop.ViewModels
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalItemCount)
        {
            AddRange(source);

            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItemCount = totalItemCount;
            TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int TotalItemCount { get; private set; }

        public int TotalPageCount { get; private set; }

        public bool HasPreviousPage
        {
            get
            {
                return PageIndex > 1;
            }
        }

        public bool HasNextPage
        {
            get
            {
                return PageIndex < TotalPageCount;
            }
        }
    }
}