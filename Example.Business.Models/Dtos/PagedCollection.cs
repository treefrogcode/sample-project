using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Business.Models.Dtos
{
    public class PagedResults<T>
    {
        public IEnumerable<T> Results { get; set; }

        public Paging Paging { get; set; }
    }
}