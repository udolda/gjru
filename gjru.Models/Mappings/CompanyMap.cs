using FluentNHibernate.Mapping;
using gjru.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gjru.Models.Mappings
{
    public class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {
            Id(u => u.Id).GeneratedBy.Identity();
            Map(u => u.CompanyName).Length(100);
        }

    }
}
