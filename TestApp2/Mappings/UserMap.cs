using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using TestApp2.Models;

namespace TestApp2.Mappings
{
    public class UserMap:ClassMap<User>
    {
        public UserMap()
        {
            Id(u => u.Id).GeneratedBy.Identity();
            Map(u => u.UserName).Length(30);
            Map(u => u.UserCompany).Length(30);
            Map(u => u.Password).Column("PasswordHash");
            //Map(u => u.PhoneNumber);
            Map(u => u.Role).Nullable();
        }
    }
}