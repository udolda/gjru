using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp2.Filters
{
    public class Range<T>
    {
        public T From { get; set; }

        public T To { get; set; }
    }
}