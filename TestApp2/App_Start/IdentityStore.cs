//using Agency.Extensions;
//using Agency.Models.Models;
//using Agency.Models.Repository;
using Microsoft.AspNet.Identity;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TestApp2.Models;

namespace TestApp2.App_Start
{
    public class IdentityStore :
         IUserStore<User, long>,
         IUserLoginStore<User, long>,
         IUserPasswordStore<User, long>,
         IUserLockoutStore<User, long>,
         IUserTwoFactorStore<User, long>,
        IUserRoleStore<User, long>
    {
        private readonly ISession session;

        public IdentityStore(ISession session)
        {
            this.session = session;
        }

        #region IUserStore<User, int>
        public Task CreateAsync(User user)
        {
            //AddToRoleAsync(user, user.Role.ToString());
            return Task.Run(() => session.SaveOrUpdate(user));
        }

        public Task DeleteAsync(User user)
        {
            return Task.Run(() => session.Delete(user));
        }

        public Task<User> FindByIdAsync(long userId)
        {
            return Task.Run(() => session.Get<User>(userId));
        }

        public Task<User> FindByNameAsync(string username)
        {
            return Task.Run(() =>
            {
                return session.QueryOver<User>()
                    .Where(u => u.UserName == username)
                    .SingleOrDefault();
            });
        }

        public Task UpdateAsync(User user)
        {
            return Task.Run(() => session.SaveOrUpdate(user));
        }
        #endregion

        #region IUserPasswordStore<User, int>
        public Task SetPasswordHashAsync(User user, string passwordHash)
        {
            return Task.Run(() => user.Password = passwordHash);
        }

        public Task<string> GetPasswordHashAsync(User user)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user)
        {
            return Task.FromResult(true);
        }
        #endregion

        #region IUserLockoutStore<User, int>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(User user)
        {
            return Task.FromResult(DateTimeOffset.MaxValue);
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset lockoutEnd)
        {
            return Task.CompletedTask;
        }

        public Task<int> IncrementAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task ResetAccessFailedCountAsync(User user)
        {
            return Task.CompletedTask;
        }

        public Task<int> GetAccessFailedCountAsync(User user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }

        public Task SetLockoutEnabledAsync(User user, bool enabled)
        {
            return Task.CompletedTask;
        }
        #endregion

        #region IUserTwoFactorStore<User, int>
        public Task SetTwoFactorEnabledAsync(User user, bool enabled)
        {
            return Task.CompletedTask;
        }

        public Task<bool> GetTwoFactorEnabledAsync(User user)
        {
            return Task.FromResult(false);
        }
        #endregion

        public void Dispose()
        {
            //do nothing
        }

        public Task AddLoginAsync(User user, UserLoginInfo login)
        {
            return Task.CompletedTask;
        }

        public Task RemoveLoginAsync(User user, UserLoginInfo login)
        {
            return Task.CompletedTask;
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user)
        {
            return new List<UserLoginInfo> { new UserLoginInfo("identityStore", user.UserName) };
        }

        public async Task<User> FindAsync(UserLoginInfo login)
        {
            return null;
        }

        public Task AddToRoleAsync(User user, string roleName) //реализация интерфейса IUserRoleStore
        {
            //править
            //user.Role = RoleExtensions.RoleCheck(roleName);
            return Task.Run(() => session.SaveOrUpdate(user));//session.Flush()); 
        }

        public Task RemoveFromRoleAsync(User user, string roleName)
        {
            //править
            //user.Role = Models.Role.None;
            return Task.Run(() => session.SaveOrUpdate(user));
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return new List<string> { user.Role.ToString() };// user.Role as List<string>; - почему-то не работает(
        }

        public Task<bool> IsInRoleAsync(User user, string roleName)
        {
            return Task.FromResult(true);
            //править
            //return Task.FromResult(RoleExtensions.RoleCheck(roleName).Equals(user.Role.ToString()));
        }

        public Task<User> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}