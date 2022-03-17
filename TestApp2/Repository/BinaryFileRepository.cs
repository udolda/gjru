using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp2.Filters;
using TestApp2.Models;

namespace TestApp2.Repository
{
    public class BinaryFileRepository : Repository<BinaryFile, BaseFilter>
    {
        public BinaryFileRepository(ISession session) :
        base(session)
        {

        }

    }
}
