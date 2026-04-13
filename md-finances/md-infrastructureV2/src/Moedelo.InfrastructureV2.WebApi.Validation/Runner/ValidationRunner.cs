using System;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.WebApi.Validation.Exceptions;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Runner
{
    [InjectAsSingleton]
    public class ValidationRunner : IValidationRunner
    {
        public void Validate(Action<ModelStateDictionary> action)
        {
            var result = new ModelStateDictionary();

            action(result);

            if (!result.IsValid)
            {
                throw new ValidationFailureException(result);
            }
        }

        public async Task ValidateAsync(Func<ModelStateDictionary, Task> action)
        {
            var result = new ModelStateDictionary();

            await action(result).ConfigureAwait(false);

            if (!result.IsValid)
            {
                throw new ValidationFailureException(result);
            }
        }
    }
}