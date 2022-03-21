using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gjru.Models.Models
{
    public class Candidate
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }

        public virtual DateTime DateofBirth { get; set; }

        [Display(Name = "Фото")]
        public virtual User User { get; set; }

        public virtual BinaryFile Avatar { get; set; }

        public virtual IList<Experience> Experience { get; set; }
    }
}
