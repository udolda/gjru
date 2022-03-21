using FluentNHibernate.Mapping;
using gjru.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gjru.Models.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(u => u.Id).GeneratedBy.Identity();
            Map(u => u.UserName).Length(30);
            References(u => u.UserCompany);
            Map(u => u.Password).Column("PasswordHash");
            //Map(u => u.PhoneNumber);
            Map(u => u.Role).Nullable();
        }

    }
}
