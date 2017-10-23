using Core.Common.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace Example.Business.Logic.Utils
{
    public static class CollectionUtils
    {
        public static PagedResults<T> PageResults<T>(IEnumerable<T> results, int page, int pageSize)
        {
            var paging = new Paging { Page = page, PageSize = pageSize, TotalRecords = results.Count() };

            results = GetPage(results, page, pageSize);

            return new PagedResults<T> { Results = results, Paging = paging };
        }

        private static IEnumerable<T> GetPage<T>(IEnumerable<T> list, int page, int pageSize)
        {
            if (pageSize > 0)
            {
                return list.Skip(pageSize * (page - 1)).Take(pageSize);
            }
            else
            {
                return list;
            }
        }
    }
}