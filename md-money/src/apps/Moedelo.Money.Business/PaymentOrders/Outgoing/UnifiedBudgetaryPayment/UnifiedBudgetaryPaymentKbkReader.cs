using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(UnifiedBudgetaryPaymentKbkReader))]
    internal class UnifiedBudgetaryPaymentKbkReader
    {
        private static readonly BudgetaryAccountCodes accountCode = BudgetaryAccountCodes.UnifiedBudgetaryPayment;

        private readonly IKbkReader kbkReader;

        public UnifiedBudgetaryPaymentKbkReader(
            IKbkReader kbkReader)
        {
            this.kbkReader = kbkReader;
        }

        public async Task<Kbk> GetMainAsync()
        {
            // костыль, хз как фиксить
            var enpStartDate = new DateTime(2023, 1, 1);
            var kbk = await kbkReader.GetKbkByAccountCodeAsync(new BudgetaryKbkRequest
            {
                AccountCode = accountCode,
                Date = enpStartDate,
                PaymentType = KbkPaymentType.Payment,
                Period = new Domain.Operations.BudgetaryPeriod { Type = BudgetaryPeriodType.Date, Date = enpStartDate }
            });
            return kbk.Single();
        }
    }
}
