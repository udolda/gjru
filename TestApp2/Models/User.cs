using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp2.Models
{
    public class User : IUser<long>
    {
        public virtual string Mail { get; set;}

        public virtual string Password
        {
            get { return ""; }
            set { }
        }

        public enum role
        {
            Admin = 0,
            HR = 1,
            Jobseeker = 2,
            Employee = 3
        }
        public virtual role Role { get; set; }

        public virtual long Id { get; set; }

        public virtual string UserName { get { return UserName; } set { UserName = Mail; } }
    }
}