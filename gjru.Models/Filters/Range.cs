using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gjru.Models.Filters
{
    public class Range<T>
    {
        [DataType(DataType.Date)]
        [Display(Name = "От")]
        public T From { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "До")]
        public T To { get; set; }
    }
}
