using System.Web.Mvc;
using Moedelo.CommonV2.Auth.Domain;

namespace Moedelo.Finances.WebApp.Infrastructure.Mvc
{
    public class MvcAuthentificationFilter : CommonV2.Auth.Mvc.MvcAuthentificationFilter
    {
        public MvcAuthentificationFilter() : base(
            (IAuthenticationService) DependencyResolver.Current.GetService(typeof (IAuthenticationService)),
            (IUserFirmContextInitializer) DependencyResolver.Current.GetService(typeof (IUserFirmContextInitializer)))
        {
        }
    }
}