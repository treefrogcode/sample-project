using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Example.Business.Models.Dtos
{
    public class Paging
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }

        public IEnumerable<T> GetPage<T>(IEnumerable<T> list)
        {
            if (PageSize > 0)
            {
                return list.Skip(PageSize * (Page - 1)).Take(PageSize);
            }
            else
            {
                return list;
            }
        }
    }
}