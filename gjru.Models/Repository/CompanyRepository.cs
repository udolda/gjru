using gjru.Models.Filters;
using gjru.Models.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gjru.Models.Repository
{
    public class CompanyRepository : Repository<Company, BaseFilter>
    {
        public CompanyRepository(ISession session) :
            base(session)
        {

        }

        /// <summary>
        /// Метод получает значение компании по ее названию
        /// </summary>
        /// <param name="selected">Company Name</param>
        /// <returns></returns>
        public Company GetCompany(string selected)
        {
            var crit = session.CreateCriteria<Company>();
            crit.Add(Restrictions.Eq("CompanyName", selected));
            return crit.List<Company>().FirstOrDefault();
        }

    }
}
