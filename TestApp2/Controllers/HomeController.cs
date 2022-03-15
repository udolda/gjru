using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.OAuth;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestApp2.Models;
using TestApp2.Repository;
using static TestApp2.Models.TokenMolel;



namespace TestApp2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private UserManager _userManager;
        private UserRepository userRepository;

        public HomeController(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ActionResult Index()
        {
            //Строки для локального тестирования
            //var tokens = new TokenModel();
            //tokens.AccessToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Im9PdmN6NU1fN3AtSGpJS2xGWHo5M3VfVjBabyJ9.eyJuYW1laWQiOiJiYTYxM2E0NC0zM2M5LTY0MGUtYjg0Yy1kNWFlNGU3NmIwMzIiLCJzY3AiOiJ2c28uYWdlbnRwb29scyB2c28uYW5hbHl0aWNzIHZzby5hdWRpdGxvZyB2c28uYnVpbGQgdnNvLmNvZGUgdnNvLmRhc2hib2FyZHMgdnNvLmVudGl0bGVtZW50cyB2c28uZXh0ZW5zaW9uIHZzby5leHRlbnNpb24uZGF0YSB2c28uZ3JhcGggdnNvLmlkZW50aXR5IHZzby5sb2FkdGVzdCB2c28ubm90aWZpY2F0aW9uX2RpYWdub3N0aWNzIHZzby5wYWNrYWdpbmcgdnNvLnByb2plY3QgdnNvLnJlbGVhc2UgdnNvLnNlcnZpY2VlbmRwb2ludCB2c28uc3ltYm9scyB2c28udGFza2dyb3Vwc19yZWFkIHZzby50ZXN0IHZzby50b2tlbmFkbWluaXN0cmF0aW9uIHZzby50b2tlbnMgdnNvLnZhcmlhYmxlZ3JvdXBzX3JlYWQgdnNvLndpa2kgdnNvLndvcmsiLCJhdWkiOiJlMDFlNTQ4Zi1lZGQ0LTQyNzMtOTkwNy0xOTQ5YmI5ZGNiMmUiLCJhcHBpZCI6IjY4Y2UxOTJlLTM2YmEtNGJhZS1iNGJkLTA3NTY2ODViN2I1YSIsImlzcyI6ImFwcC52c3Rva2VuLnZpc3VhbHN0dWRpby5jb20iLCJhdWQiOiJhcHAudnN0b2tlbi52aXN1YWxzdHVkaW8uY29tIiwibmJmIjoxNjA4ODg3NjA0LCJleHAiOjE2MDg4OTEyMDR9.wnLxLZNXOsPdBbN5J5SX13vzbdKiWJdNW6FX7cYYpNHAQRrINT2UfAfhkK7e6CwkeDPcNNND_p636ToD6x9fvADN_T-VfLFWASrgmi0uFvypIRqj2IZWMPui5_10CA2oOPrkzZF5kRbXL4-RyeVOYRWxm464whjanFKOJ4fQYsVeqKU9n1TRfkgLmXqo5-G2bZSeJsowZqiji5MB1uUp6NYaFFW9u8x1S2Nd70NcAdBPQNT2T1fCixm9Zbp4rck7ouVsQx1pYA0iv-SDr38afbADTtFIJa9oRD1gLf-J4Ojb8cngE8Z-0ZkYLywj3U4I9F7o3Flu12vV68LhYrtwjw";
            //Session["token"] = tokens;
            if (Session["token"] != null)
            {
                Session["access"] = true;
                var token = (TokenModel)Session["token"];
                VssConnection connection = new VssConnection(new Uri("https://dev.azure.com/LATeamInc/"), new VssOAuthAccessTokenCredential(token.AccessToken));
                Session["connect"] = connection;
                Session["info"] += connection.HasAuthenticated.ToString() + " " + connection.AuthorizedIdentity.DisplayName + " " + token.AccessToken;
                try
                {
                    var witClient = connection.GetClient<WorkItemTrackingHttpClient>();
                    var hierarchyAccess = witClient.GetQueriesAsync("WorkPractice", depth: 2).Result;
                }
                catch (Exception)
                {
                    Session["access"] = false;
                }

            }
            return View();
        }

        /// <summary>
        /// Переход на страницу регистрации
        /// </summary>
        /// <returns>HTML страница регитрации</returns>
        public ActionResult Register()
        {
            RegistrationViewModel model = new RegistrationViewModel();

            return View(model);
        }

        /// <summary>
        /// Стандартный метод для регистрации, дополнен с учетом наличия ролей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegistrationViewModel model)
        {
            if (userRepository.FindByLogin("admin") == null)
            {
                var user = new User { UserName = "admin", Password = "Gibibl666", Role = role.Admin };
                var result = await UserManager.CreateAsync(user, model.Password);
            }

            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Password = model.Password, Role = model.Role };
                var result = await UserManager.CreateAsync(user, model.Password);

                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");


                return RedirectToAction("Main", String.Format("{0}", model.Role.ToString()));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Переход на страницу с информацией
        /// </summary>
        /// <returns>HTML страница информации</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Поддробности";

            return View();
        }

        /// <summary>
        /// Вовзвращает Manage отображение
        /// </summary>
        /// <returns></returns>
        public ActionResult Manage()
        {
            ViewBag.HasAccess = Session["access"];
            
            return View("Manage");
        }

        /// <summary>
        /// Возврат на домашнюю страницу после выхода из аккаунта
        /// </summary>
        /// <returns>HTML главная страница</returns>
        //[ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Home");
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public UserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /// <summary>
        /// Метод для перебрасывания в случае проблем со входом
        /// </summary>
        /// <returns></returns>
        public ActionResult EnterError()
        {
            ViewBag.Message = @"Кажется, возникли проблемы со входом. Попробуйте перезапустить приложение 
                                или выйти из учетной записи и зайти снова";
            return View();
        }

        /// <summary>
        /// /Account/Login
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Standart login method. Modified for the use of roles.
        /// </summary>
        /// <param name="model">Model of login view. Contains login and password</param>
        /// <param name="returnUrl">Url corresponding to the result of the login operation</param>
        /// <returns>View</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    try
                    {
                        var role = UserManager.GetRoles(Convert.ToInt64(User.Identity.GetUserId())).SingleOrDefault();
                        //иногда возникает проблема с созданием UserManager. Выглядит как проблема из коробки. Но оно работает само по себе
                        return RedirectToAction("Main", String.Format("{0}", role.ToString()));
                    }
                    catch
                    {
                        //править
                        return RedirectToAction("Redirect", "Common");
                    }

                case SignInStatus.LockedOut: return View("Lockout");

                case SignInStatus.RequiresVerification: return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //править
        ////
        //// GET: /Account/VerifyCode
        //[AllowAnonymous]
        //public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        //{
        //    // Require that the user has already logged in via username/password or external login
        //    if (!await SignInManager.HasBeenVerifiedAsync())
        //    {
        //        return View("Error");
        //    }
        //    return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        //}

        //править
        ////
        //// POST: /Account/VerifyCode
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // The following code protects for brute force attacks against the two factor codes. 
        //    // If a user enters incorrect codes for a specified amount of time then the user account 
        //    // will be locked out for a specified amount of time. 
        //    // You can configure the account lockout settings in IdentityConfig
        //    var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(model.ReturnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid code.");
        //            return View(model);
        //    }
        //}

        /// <summary>
        /// GET: /Account/ConfirmEmail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(long userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //править
        ////
        //// POST: /Account/ForgotPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await UserManager.FindByNameAsync(model.Email);
        //        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        //        {
        //            // Don't reveal that the user does not exist or is not confirmed
        //            return View("ForgotPasswordConfirmation");
        //        }

        //        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
        //        // Send an email with this link
        //        // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
        //        // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
        //        // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
        //        // return RedirectToAction("ForgotPasswordConfirmation", "Account");
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        /// <summary>
        /// GET: /Account/ForgotPasswordConfirmation
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// GET: /Account/ResetPassword
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //править
        ////
        //// POST: /Account/ResetPassword
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    var user = await UserManager.FindByNameAsync(model.Email);
        //    if (user == null)
        //    {
        //        // Don't reveal that the user does not exist
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
        //    if (result.Succeeded)
        //    {
        //        return RedirectToAction("ResetPasswordConfirmation", "Account");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

        /// <summary>
        /// GET: /Account/ResetPasswordConfirmation
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {

            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

    }
}