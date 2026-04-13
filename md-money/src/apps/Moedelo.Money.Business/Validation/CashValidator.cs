using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Cash;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(ICashValidator))]
    internal sealed class CashValidator : ICashValidator
    {
        private readonly ICashReader cashReader;

        public CashValidator(ICashReader cashReader)
        {
            this.cashReader = cashReader;
        }

        public async Task ValidateAsync(long cashId)
        {
            var cash = await cashReader.GetByIdAsync(cashId);
            if (cash == null)
            {
                throw new BusinessValidationException("CashId", $"Не найдена касса с идентификатором {cashId}");
            }
            if (cash.SubcontoId == null)
            {
                throw new BusinessValidationException("CashId", $"Отсутствует субконто кассы с идентификатором {cashId}");
            }
        }
    }
}
