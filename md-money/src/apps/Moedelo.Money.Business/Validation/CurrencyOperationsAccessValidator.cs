using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.Operations;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(ICurrencyOperationsAccessValidator))]
    internal class CurrencyOperationsAccessValidator : ICurrencyOperationsAccessValidator
    {
        private readonly IOperationsAccessReader accessReader;

        public CurrencyOperationsAccessValidator(IOperationsAccessReader accessReader)
        {
            this.accessReader = accessReader;
        }

        public async Task ValidateAsync()
        {
            var hasAccess = await accessReader.CanEditCurrencyOperations();
            if (!hasAccess)
            {
                throw new BusinessValidationException(".", "Нет доступа на создание/редактирование валютных операций")
                {
                    Reason = ValidationFailedReason.OperationTypeNotAllowed
                };
            }
        }
    }
}