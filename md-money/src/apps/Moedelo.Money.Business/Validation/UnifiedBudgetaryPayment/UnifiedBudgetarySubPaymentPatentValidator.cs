using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Business.Validation.Extensions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(UnifiedBudgetarySubPaymentPatentValidator))]
    internal class UnifiedBudgetarySubPaymentPatentValidator
    {
        public Task ValidateAsync(
            Kbk kbk,
            long? patentId,
            string prefix = null)
        {
            if (kbk.AccountCode == Enums.BudgetaryAccountCodes.Patent && !patentId.HasValue)
            {
                throw new BusinessValidationException("PatentId".WithPrefix(prefix), $"Дяя кбк {kbk.AccountCode.GetDescription()} не указан патент");
            }
            if (kbk.AccountCode != Enums.BudgetaryAccountCodes.Patent && patentId.HasValue)
            {
                throw new BusinessValidationException("PatentId".WithPrefix(prefix), $"Нельзя использовать патент c идентификатором {patentId.Value} с кбк {kbk.AccountCode.GetDescription()}");
            }

            return Task.CompletedTask;
        }
    }
}
