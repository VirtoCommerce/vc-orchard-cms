using System;
using System.Collections.Generic;

namespace VirtoCommerce.WebStore.ViewModels
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalItemsCount)
        {
            AddRange(source);

            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItemsCount = totalItemsCount;
            TotaPagesCount = (int)Math.Ceiling(totalItemsCount / (double)pageSize);
        }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int TotalItemsCount { get; private set; }

        public int TotaPagesCount { get; private set; }
    }
}