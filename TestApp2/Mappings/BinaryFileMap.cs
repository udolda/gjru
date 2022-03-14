using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp2.Models;

namespace TestApp2.Mappings
{
    public class BinaryFileMap : ClassMap<BinaryFile>
    {
        public BinaryFileMap()
        {
            Id(f => f.Id).GeneratedBy.Identity();
            Map(f => f.Name);
            Map(f => f.Path);
            Map(f => f.ContentType);
            Map(f => f.Content).Length(int.MaxValue);

        }
    }
}