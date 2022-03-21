using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gjru.Models.Models
{
    public class Experience
    {
        public virtual long Id { get; set; }

        public virtual string Skill { get; set; }

        public virtual long Duration { get; set; }

        public virtual IList<Candidate> Candidates { get; set; }

        public virtual IList<Vacancy> Vacancies { get; set; }
    }
}
