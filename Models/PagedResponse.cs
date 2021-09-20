using System;
using System.Collections.Generic;
using System.Linq;

namespace Tqviet.CoffeeShop.Models
{
    public class PagedResponse<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public string ErrorMessage { get; set; }

        public PagedResponse()
        {
        }

        public PagedResponse(List<T> items, int count, int pageNumber, int pageSize, string errorMessage = null)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            if (count == 0 && pageNumber == 0 && pageSize == 0)
            {
                TotalPages = 0;
            }
            else
            {
                TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            }

            AddRange(items);

            ErrorMessage = errorMessage;
        }

        public PagedResponse<T> ToPagedResponse(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedResponse<T>(items, count, pageNumber, pageSize);
        }
    }
}
