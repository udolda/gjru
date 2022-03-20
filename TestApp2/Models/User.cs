using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp2.Models
{
    public enum role
    {
        None = -1,
        Admin = 0,
        HR = 1,
        Jobseeker = 2,
        Employer = 3
    }

    public class User : IUser<long>
    {
        public virtual long Id { get; set; }

        public virtual string Password { get; set; }

        //public virtual long PhoneNumber { get; set; }

        public virtual role Role { get; set; }

        public virtual string UserName { get; set; }

        public virtual Status Status { get; set; }
    }
}
