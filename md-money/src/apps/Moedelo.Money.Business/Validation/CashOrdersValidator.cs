using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.CashOrders;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(ICashOrdersValidator))]
    internal sealed class CashOrdersValidator : ICashOrdersValidator
    {
        private readonly ICashOrdersReader cashOrdersReader;

        public CashOrdersValidator(ICashOrdersReader cashOrdersReader)
        {
            this.cashOrdersReader = cashOrdersReader;
        }

        public async Task ValidateAsync(long documentBaseId, OperationType? operationType, MoneyDirection? direction)
        {
            var cashOrder = await cashOrdersReader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            if (cashOrder == null)
            {
                throw new BusinessValidationException("CashOrderBaseId", $"Не найден кассовый ордер с ид {documentBaseId}");
            }
            if (operationType.HasValue && cashOrder.OperationType != operationType.Value)
            {
                throw new BusinessValidationException("CashOrderBaseId", $"Неверный тип кассового ордера с ид {documentBaseId}");
            }
            if (direction.HasValue && cashOrder.Direction != direction.Value)
            {
                var directionText = direction.Value == MoneyDirection.Incoming
                    ? "приходным"
                    : "расходным";
                throw new BusinessValidationException("CashOrderBaseId", $"Кассовый ордер с ид {documentBaseId} не является {directionText}");
            }
        }
    }
}
