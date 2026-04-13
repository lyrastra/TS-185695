using System.Web.Mvc;
using Moedelo.CommonV2.Auth.Mvc;
using Moedelo.Finances.WebApp.Infrastructure.Mvc;

namespace Moedelo.Finances.WebApp.Controllers
{
    [UnhandledException]
    [Infrastructure.Mvc.MvcAuthentificationFilter]
    [MvcRedirectUnauthorizedRequest]
    public class BaseContoroller : Controller
    {
    }
}