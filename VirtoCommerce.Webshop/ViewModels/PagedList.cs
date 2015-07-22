using System;
using System.Collections.Generic;

namespace VirtoCommerce.Webshop.ViewModels
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> source, int pageSize, int totalCount)
        {
            AddRange(source);

            PageSize = pageSize;
            TotalItemCount = totalCount;
            TotalPageCount = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        public int PageSize { get; private set; }

        public int TotalItemCount { get; private set; }

        public int TotalPageCount { get; private set; }
    }
}