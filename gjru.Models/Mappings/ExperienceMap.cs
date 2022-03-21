using FluentNHibernate.Mapping;
using gjru.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gjru.Models.Mappings
{
    public class ExperienceMap : ClassMap<Experience>
    {
        public ExperienceMap()
        {
            Id(e => e.Id).GeneratedBy.Identity();
            Map(e => e.Skill);
            Map(e => e.Duration);
            HasManyToMany(e => e.Candidates).Inverse().Table("CandidateExperience");
            HasManyToMany(e => e.Vacancies).Inverse().Table("VacancyExperience");
        }

    }
}
