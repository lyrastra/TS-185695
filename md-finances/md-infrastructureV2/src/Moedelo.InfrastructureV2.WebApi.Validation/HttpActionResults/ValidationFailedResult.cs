using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Moedelo.InfrastructureV2.WebApi.Validation.Exceptions;
using Moedelo.InfrastructureV2.WebApi.Validation.Http;

namespace Moedelo.InfrastructureV2.WebApi.Validation.HttpActionResults;

public class ValidationFailedResult : IHttpActionResult
{
    private readonly HttpRequestMessage request;
    private readonly ModelStateDictionary modelState;

    public ValidationFailedResult(HttpRequestMessage request, ValidationFailureException exception)
    {
        this.request = request;
        modelState = exception.ModelState;
    }
        
    public ValidationFailedResult(HttpRequestMessage request, ModelStateDictionary modelState)
    {
        this.request = request;
        this.modelState = modelState;
    }
        
    public ValidationFailedResult(HttpRequestMessage request, IReadOnlyDictionary<string, List<string>> modelStateDict)
    {
        if (modelStateDict == null)
        {
            throw new ArgumentNullException(nameof(modelStateDict));
        }
            
        this.request = request;
        this.modelState = new ModelStateDictionary();

        foreach (var kv in modelStateDict)
        {
            var key = kv.Key;
            foreach (var error in kv.Value.Where(x => x != null))
            {
                modelState.AddModelError(key, error);
            }
        }
    }

    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(CreateResponse());
    }

    private HttpResponseMessage CreateResponse()
    {
        return request.CreateErrorResponse(
            HttpStatusCodeEx.ValidationFailed,
            ToHttpError(modelState));
    }
        
    private static HttpError ToHttpError(ModelStateDictionary modelStateDictionary)
    {
        var httpError = new HttpError("Ошибка валидации данных");
        var validationErrors = new HttpError();

        foreach (var pair in modelStateDictionary.Where(p => p.Value.Errors?.Any() == true))
        {
            var propName = pair.Key;
            var modelState = pair.Value;

            var errorStrings = modelState.Errors
                .Where(e => !string.IsNullOrEmpty(e.ErrorMessage))
                .Select(e => e.ErrorMessage)
                .ToArray();

            if (errorStrings.Any())
            {
                validationErrors[propName] = errorStrings;
            }
        }

        httpError.Add("ValidationErrors", validationErrors.OrderBy(p => p.Key).ToDictionary(p => p.Key, p => p.Value));

        return httpError;
    }
}