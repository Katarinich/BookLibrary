using BookLibrary.Api.DAL;
using BookLibrary.Api.Managers;
using BookLibrary.Api.Services;
using BookLibrary.Api.Services.NotificationTransport;
using BookLibrary.Api.Services.Validation;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookLibrary.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();

            container.Register<BookLibraryContext, BookLibraryContext>(Lifestyle.Scoped);
            container.Register<IUserManager, UserManager>(Lifestyle.Scoped);
            container.Register<IConfirmationCodeManager, ConfirmationCodeManager>(Lifestyle.Scoped);
            container.Register<IEmailManager, EmailManager>(Lifestyle.Scoped);
            container.Register<ICodeGenerator, CodeGenerator>(Lifestyle.Scoped);
            container.Register<IPasswordHasher, PasswordHasher>(Lifestyle.Scoped);
            container.Register<INotificationTransportService, NotificationTransportService>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);
            container.Register<IJwtService, JwtService>(Lifestyle.Scoped);
            container.Register<IEmailChangeService, EmailChangeService>(Lifestyle.Scoped);
            container.Register<IAuthentificationService, AuthentificationService>(Lifestyle.Scoped);
            container.Register<IRegistrationService, RegistrationService>(Lifestyle.Scoped);
            container.Register<IConfirmationSenderService, ConfirmationSenderService>(Lifestyle.Scoped);
            container.Register<IEmailConfirmationService, EmailConfirmationService>(Lifestyle.Scoped);
            container.Register<IConfirmationCodeService, ConfirmationCodeService>(Lifestyle.Scoped);
            container.Register<ICodeValidationRule, CodeValidationRule>(Lifestyle.Scoped);
            container.Register<IPasswordChangeService, PasswordChangeService>(Lifestyle.Scoped);
            container.Register<IPasswordRecoveryService, PasswordRecoveryService>(Lifestyle.Scoped);
            container.Register<IPasswordPolicy, PasswordPolicy>(Lifestyle.Scoped);
            container.Register<IValidationService, UserDraftValidationService>(Lifestyle.Scoped);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
