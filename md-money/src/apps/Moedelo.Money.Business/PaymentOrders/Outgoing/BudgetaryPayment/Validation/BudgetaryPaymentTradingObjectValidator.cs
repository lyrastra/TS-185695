using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.TradingObjects;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentTradingObjectValidator))]
    internal class BudgetaryPaymentTradingObjectValidator : IBudgetaryPaymentTradingObjectValidator
    {
        private readonly ITradingObjectReader tradingObjectReader;

        public BudgetaryPaymentTradingObjectValidator(ITradingObjectReader tradingObjectReader)
        {
            this.tradingObjectReader = tradingObjectReader;
        }

        public async Task ValidateAsync(BudgetaryAccountCodes accountCode, int? tradingObjectId)
        {
            if (accountCode == BudgetaryAccountCodes.TradingFees && tradingObjectId.HasValue)
            {
                var tradingObject = await tradingObjectReader.GetByIdAsync(tradingObjectId.Value).ConfigureAwait(false);
                if (tradingObject == null)
                {
                    throw new BusinessValidationException("TradingObjectId", $"Не найден торговый объект с идентификатором {tradingObjectId.Value}");
                }
            }
        }
    }
}