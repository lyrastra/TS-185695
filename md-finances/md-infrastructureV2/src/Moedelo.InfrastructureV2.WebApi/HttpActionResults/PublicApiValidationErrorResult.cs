using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Moedelo.InfrastructureV2.WebApi.HttpActionResults
{
    public class PublicApiValidationErrorResult : IHttpActionResult
    {
        private const HttpStatusCode UnprocessableEntityHttpStatusCode = (HttpStatusCode)422;

        private static Regex PropertyNameRegex = new Regex(@"\w+\.(.*)", RegexOptions.Compiled);

        private readonly HttpRequestMessage request;
        private readonly ModelStateDictionary modelState;

        public PublicApiValidationErrorResult(HttpRequestMessage request, ModelStateDictionary modelState)
        {
            this.request = request;
            this.modelState = modelState;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            IDictionary<string, object> errors = new ExpandoObject();
            foreach (var keyValuePair in modelState)
            {
                var property = PropertyNameRegex.Replace(keyValuePair.Key, "$1");
                var messages = keyValuePair.Value.Errors
                    .Where(e => !string.IsNullOrEmpty(e.ErrorMessage))
                    .Select(x => x.ErrorMessage);
                errors.Add(property, messages);
            }
            var response = request.CreateResponse(UnprocessableEntityHttpStatusCode, new { errors });

            return Task.FromResult(response);
        }
    }
}