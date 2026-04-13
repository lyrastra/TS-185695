using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.Validation;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(BudgetaryPaymentImportValidator))]
    internal sealed class BudgetaryPaymentImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IBudgetaryPaymentKbkValidator kbkValidator;
        private readonly IBudgetaryPaymentTradingObjectValidator tradingObjectValidator;
        private readonly IBudgetaryPeriodValidator periodValidator;

        public BudgetaryPaymentImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            IBudgetaryPaymentKbkValidator kbkValidator,
            IBudgetaryPaymentTradingObjectValidator tradingObjectValidator,
            IBudgetaryPeriodValidator periodValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kbkValidator = kbkValidator;
            this.tradingObjectValidator = tradingObjectValidator;
            this.periodValidator = periodValidator;
        }

        public async Task ValidateAsync(BudgetaryPaymentImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            await kbkValidator.ValidateAsync(request.AccountCode, request.KbkId, request.KbkNumber);
            await tradingObjectValidator.ValidateAsync(request.AccountCode, request.TradingObjectId);
            await periodValidator.ValidateAsync(request.Period);
        }
    }
}