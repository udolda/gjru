using TestApp2.Filters;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace TestApp2.Repository
{
    public class Repository<T, FT>
       where T : class
       where FT : BaseFilter

    {
        protected ISession session;

        public Repository(ISession session)
        {
            this.session = session;
        }

        public virtual T Load(long id)
        {
            return session.Load<T>(id);
        }

        protected virtual void SetFetchOptions(ICriteria crit, FetchOptions options)
        {
            if (!string.IsNullOrEmpty(options.SortExpression))
            {
                crit.AddOrder(options.SortDirection == SortDirection.Ascending ?
                    Order.Asc(options.SortExpression) :
                    Order.Desc(options.SortExpression));
            }
        }

        public IList<T> Find(FT filter, FetchOptions options = null)
        {
            var crit = session.CreateCriteria<T>();
            SetupFilter(crit, filter);
            SetFetchOptions(crit, options);
            return crit.List<T>();
        }

        public virtual void SetupFilter(ICriteria crit, FT filter)
        {

        }

        public virtual IList<T> GetAll()
        {
            return session.CreateCriteria<T>().List<T>();
        }

        public virtual void Save(T entity)
        {
            using (var tr = session.BeginTransaction())
            {
                session.SaveOrUpdate(entity);
                tr.Commit();
            }

        }

        public IList<T> GetAllWithSort(FetchOptions options)
        {
            var crit = session.CreateCriteria<T>();
            SetFetchOptions(crit, options);
            return crit.List<T>();
        }

    }
}
