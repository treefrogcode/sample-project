using System.Collections.Generic;

namespace Core.Common.Dtos
{
    public class PagedResults<T>
    {
        public IEnumerable<T> Results { get; set; }

        public Paging Paging { get; set; }
    }
}