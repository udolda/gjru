using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp2.Models
{
    public class Company
    {
        public virtual long Id { get; set; }

        public virtual string CompanyName { get; set; }
    }
}