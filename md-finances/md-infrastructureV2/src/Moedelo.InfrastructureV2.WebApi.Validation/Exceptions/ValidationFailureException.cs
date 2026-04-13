using System;
using System.Web.Http.ModelBinding;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Exceptions;

public class ValidationFailureException : Exception
{
    public ValidationFailureException(ModelStateDictionary modelState)
    {
        ModelState = modelState;
    }

    public ModelStateDictionary ModelState { get; }
}