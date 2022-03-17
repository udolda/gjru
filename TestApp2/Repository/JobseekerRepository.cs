using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApp2.Filters;
using TestApp2.Models;

namespace TestApp2.Repository
{
    public class JobseekerRepository : Repository<Candidate, JobseekersFilter>
    {
        public JobseekerRepository(ISession session) : base(session)
        {

        }

        /// <summary>
        /// Метод осуществляет поиск анкеты текущего пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Candidate FindProfile(long userId)
        {
            var crit = session.CreateCriteria<Candidate>();
            crit.Add(Restrictions.Eq("User.Id", userId));
            return crit.List<Candidate>().FirstOrDefault();
        }

        /// <summary>
        /// Метод находит соискателя, подходящего под параметры вакансии
        /// </summary>
        /// <param name="experiences"></param>
        /// <returns></returns>
        public IList<Candidate> FindSuitableCandidate(List<long> experiences)
        {
            var crit = session.CreateCriteria<Candidate>()
                .CreateAlias("Experience", "CandidateExperience")
                .Add(Restrictions.In("CandidateExperience.id", experiences));
            
            return crit.List<Candidate>();
        }

    }
}