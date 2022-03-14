using TestApp2.Controllers;
using TestApp2.Repository;
using TestApp2.Models;
using Autofac;
using Autofac.Integration.Mvc;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using Owin;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(TestApp2.Startup))]
[assembly: OwinStartup(typeof(TestApp2.Startup))]
namespace TestApp2
{
    public partial class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MSSQL"];
            if (connectionString == null) { throw new Exception("not found"); }
            
            var builder = new ContainerBuilder();
            builder.Register(x =>
            {
                var cfg = Fluently.Configure()
                                .Database(MsSqlConfiguration.MsSql2012
                                .ConnectionString(connectionString.ConnectionString)
                                .Dialect<MsSql2012Dialect>())
                                .Mappings(m => {
                                    m.FluentMappings.AddFromAssemblyOf<User>();
                                })
                                .CurrentSessionContext("call");
                var schemaExport = new SchemaUpdate(cfg.BuildConfiguration());
                schemaExport.Execute(true, true);

                return cfg.BuildSessionFactory();
            }).As<ISessionFactory>().SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerRequest().InstancePerLifetimeScope();
            builder.RegisterControllers(Assembly.GetAssembly(typeof(HomeController)));
            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterGeneric(typeof(Repository<,>));
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(UserRepository)))
                .AsSelf()
                .AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(UserManager)));


            var container = builder.Build().BeginLifetimeScope();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(UserManager)))
            //    .AsSelf()
            //    .AsImplementedInterfaces();


            //.AsSelf()
            //.AsImplementedInterfaces();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            app.UseAutofacMiddleware(container);
            try
            {
                var CreateProcedure = @"CREATE PROCEDURE [dbo].[sp_InsertVacancy] @VacancyName nvarchar(100),
                                        @VacancyDescription nvarchar(1000), @Starts DateTime2,@Ends DateTime2, @Creator_id bigint, @Status nvarchar(255),
                                        @Company_id bigint AS INSERT INTO [Vacancy] (VacancyName, VacancyDescription, Starts, Ends, Creator_id, Status, Company_id )
                                        VALUES ( @VacancyName, @VacancyDescription, @Starts, @Ends, @Creator_id, @Status, @Company_id ) SELECT SCOPE_IDENTITY() ";

                var result = container.Resolve<ISession>().CreateSQLQuery(CreateProcedure); ;
                result.ExecuteUpdate();

            }
            catch (Exception ex)
            { }
            app.CreatePerOwinContext(() => new UserManager(new App_Start.IdentityStore(DependencyResolver.Current.GetServices<ISession>().FirstOrDefault())));
            app.CreatePerOwinContext<ApplicationSignInManager>((options, context) => new ApplicationSignInManager(context.GetUserManager<UserManager>(), context.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
                Provider = new CookieAuthenticationProvider()
            });

        }

    }
}
