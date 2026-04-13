using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(NumberValidator))]
    internal sealed class NumberValidator
    {
        const int MaxLength = 20;
        const int MinLength = 6;

        public Task ValidateAsync(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return Task.CompletedTask;
            }
            if (number.Length > MaxLength)
            {
                throw new BusinessValidationException("Number", $"Длина номера не должна превышать {MaxLength} символов.");
            }
            return Task.CompletedTask;
        }

        public Task ValidatePaymentOrderAsync(bool isFromImport, string number)
        {
            if (isFromImport || string.IsNullOrWhiteSpace(number))
            {
                return Task.CompletedTask;
            }
            if (number.Length > MinLength)
            {
                throw new BusinessValidationException("Number", $"Длина номера не должна превышать {MinLength} символов.");
            }
            return Task.CompletedTask;
        }
    }
}
