using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;
using Moedelo.CommonV2.Auth.Basic.WebApi.Adapter;

namespace Moedelo.CommonV2.Auth.Basic.WebApi
{
    public class BasicAuthorizationAttribute : AuthorizationFilterAttribute
    {
        public IBasicAuthValidator basicAuthValidator { get; set; }

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization == null)
                {
                    throw new Exception("Empty Authorization header!");
                }

                // Gets header parameters  
                string authenticationString = actionContext.Request.Headers.Authorization.Parameter;
                string originalString = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationString));

                // Gets username and password
                var splittedString = originalString.Split(':');
                if (splittedString.Length != 2)
                {
                    throw new Exception("Invalid parameters in Authorization header!");
                }
                string userName = splittedString[0];
                string password = splittedString[1];

                // Validate username and password  
                if (!basicAuthValidator.ValidateAsync(userName, password).Result)
                {
                    throw new Exception("Invalid user or password!");
                }

                base.OnAuthorization(actionContext);
            }
            catch (Exception e)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, e.Message);
                actionContext.Response.Headers.Add("WWW-Authenticate", "Basic realm=\"oauth.moedelo.org\"");
            }
        }
    }
}