using gjru.Models.Filters;
using gjru.Models.Models;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.Criterion;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;

namespace gjru.Models.Repository
{
    public class UserRepository : Repository<User, JobseekersFilter>
    {
        public UserRepository(ISession session) :
            base(session)
        {

        }

        /// <summary>
        /// Метод осуществляет поиск пользователя по его логину
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public User FindByLogin(string login)
        {
            var crit = session.CreateCriteria<User>();
            crit.Add(Restrictions.Eq("Login", login));
            try
            {
                return crit.List<User>().FirstOrDefault();
            }
            catch
            {
                User user = null;
                return user;
            }
        }

        /// <summary>
        /// Позволяет получить текущего пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User GetCurrentUser(IPrincipal user = null)
        {
            user = user ?? (HttpContext.Current != null ? HttpContext.Current.User : Thread.CurrentPrincipal);
            if (user == null || user.Identity == null)
            {
                return null;
            }
            var currentUserId = user.Identity.GetUserId();
            long userId;
            if (string.IsNullOrEmpty(currentUserId) || !long.TryParse(currentUserId, out userId))
            {
                return null;
            }
            return session.Get<User>(userId);
        }

    }
}
