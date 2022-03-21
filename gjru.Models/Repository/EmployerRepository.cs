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
    public class EmployerRepository : Repository<Vacancy, VacancyFilter>
    {
        public EmployerRepository(ISession session) :
            base(session)
        {

        }

        /// <summary>
        /// Метод ищет все вакансии конкретного HR с учетом фильтра
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public IList<Vacancy> ShowMyVacancies(long userId, VacancyFilter filter, FetchOptions options)
        {
            var crit = session.CreateCriteria<Vacancy>();
            SetupFilter(crit, filter);
            crit.Add(Restrictions.Eq("Creator.Id", userId));
            if (options != null)
            {
                SetFetchOptions(crit, options);
            }
            return crit.List<Vacancy>();
        }

        /// <summary>
        /// Метод для сохранения новой вакансии
        /// </summary>
        /// <param name="vacancy"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public long SaveWProcedure(Vacancy vacancy, long Id)
        {
            var query = session.CreateSQLQuery("exec sp_InsertVacancy :VacancyName, :VacancyDescription, :Starts, :Ends, :User_id, :Status, :Company_id")
                    .SetParameter("VacancyName", vacancy.VacancyName)
                    .SetParameter("VacancyDescription", vacancy.VacancyDescription)
                    .SetParameter("Starts", vacancy.Starts)
                    .SetParameter("Ends", vacancy.Ends)
                    .SetParameter("User_id", Id)
                    .SetParameter("Status", vacancy.Status)
                    .SetParameter("Company_id", vacancy.Company.Id)
                    ;
            var result = query.UniqueResult();

            return long.Parse(result.ToString());
            //здесь нет опыта, потому что нужно передавать множественный параметр. Делается оно через XML, но я пока такое не умею
            //Опыт записывается через апдейт, который идет после
        }

        /// <summary>
        /// Метод задает критерии поиска вакансий по фильтру
        /// </summary>
        /// <param name="crit"></param>
        /// <param name="filter"></param>
        public override void SetupFilter(ICriteria crit, VacancyFilter filter)
        {
            if (filter != null)
            {
                if (filter.SearchString != null)
                {
                    crit.Add(Restrictions.Like("VacancyName", filter.SearchString, MatchMode.Anywhere));
                }
                if (filter.Experience != null)
                {
                    List<long> exp = new List<long>();
                    foreach (var e in filter.Experience)
                    {
                        exp.Add(e.Id);
                    }
                    crit.CreateAlias("Requirements", "VacancyExperience")
                        .Add(Restrictions.In("VacancyExperience.id", exp));
                }
                if (filter.StartDateRange != null)
                {
                    if (filter.StartDateRange.From.HasValue)
                    {
                        crit.Add(Restrictions.Ge("Starts", filter.StartDateRange.From.Value));
                    }
                    if (filter.StartDateRange.To.HasValue)
                    {
                        crit.Add(Restrictions.Le("Starts", filter.StartDateRange.To.Value));
                    }
                }
                if (filter.CompanyName.Id != 0)
                {
                    crit.Add(Restrictions.Eq("Company.id", filter.CompanyName.Id));
                }
                if (filter.EndDateRange != null)
                {
                    if (filter.EndDateRange.From.HasValue)
                    {
                        crit.Add(Restrictions.Ge("Ends", filter.EndDateRange.From.Value));
                    }
                    if (filter.StartDateRange.To.HasValue)
                    {
                        crit.Add(Restrictions.Le("Ends", filter.EndDateRange.To.Value));
                    }
                }
                if (filter.Statuses != null)
                {
                    foreach (var s in filter.Statuses)
                    {
                        crit.Add(Restrictions.Eq("Status", filter.Statuses));
                    }
                }
            }
        }

        /// <summary>
        /// Получает вакансии с учетом фильтрации
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IList<Vacancy> GetVacanciesFiltered(VacancyFilter filter)
        {
            var crit = session.CreateCriteria<Vacancy>();
            SetupFilter(crit, filter);

            return crit.List<Vacancy>();
        }

        /// <summary>
        /// Метод создает критерий для поиска подходящей вакансии
        /// </summary>
        /// <param name="experiences"></param>
        /// <returns></returns>
        public IList<Vacancy> FindSuitableVacancy(List<long> experiences)
        {
            var crit = session.CreateCriteria<Vacancy>()
                .CreateAlias("Requirements", "VacancyExperience")
                .Add(Restrictions.In("VacancyExperience.id", experiences));

            return crit.List<Vacancy>();
        }

    }
}
