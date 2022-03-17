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
    public class ExperienceRepository : Repository<Experience, BaseFilter>
    {
        public ExperienceRepository(ISession session) : base(session)
        {

        }

        /// <summary>
        /// Метод получает выбранный опыт
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public IList<Experience> GetSelectedExperience(List<long> items)
        {
            var crit = session.CreateCriteria<Experience>();
            crit.Add(Restrictions.In("Id", items));
            return crit.List<Experience>();
        }

        /// <summary>
        /// Метод для создания нового пункта опыта путем ввода его текста
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public IList<long> CreateNewExperience(string exp)
        {
            List<string> newExp = new List<string>();
            List<long> experiences = new List<long>();
            newExp.AddRange(exp.Split(';'));
            foreach (var e in newExp)
            {
                Experience experience = new Experience
                {
                    Skill = e
                };
                Save(experience);
                experiences.Add(experience.Id);
            }
            return experiences;
        }

    }
}