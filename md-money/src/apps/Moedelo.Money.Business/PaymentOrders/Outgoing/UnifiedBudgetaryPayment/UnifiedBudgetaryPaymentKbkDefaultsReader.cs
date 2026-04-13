using Moedelo.Accounting.Domain.Shared.PaymentOrders.Outgoing.BudgetaryPayments;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentKbkDefaultsReader))]
    internal class UnifiedBudgetaryPaymentKbkDefaultsReader : IUnifiedBudgetaryPaymentKbkDefaultsReader
    {
        private readonly UnifiedBudgetaryPaymentKbkReader kbkReader;
        private readonly BudgetaryRecipientRequisitesReader recipientRequisitesReader;

        public UnifiedBudgetaryPaymentKbkDefaultsReader(
            UnifiedBudgetaryPaymentKbkReader kbkReader,
            BudgetaryRecipientRequisitesReader recipientRequisitesReader)
        {
            this.kbkReader = kbkReader;
            this.recipientRequisitesReader = recipientRequisitesReader;
        }

        public async Task<BudgetaryKbkDefaultsResponse> GetAsync(UnifiedBudgetaryPaymentKbkDefaultsRequest request)
        {
            var kbk = await kbkReader.GetMainAsync();
            if (kbk == null)
            {
                return new BudgetaryKbkDefaultsResponse();
            }

            var fundsRequisites = await ResolveOrderDetailsForDefaultFieldsAsync();

            return new BudgetaryKbkDefaultsResponse
            {
                PayerStatus = BudgetaryPayerStatus.Company,
                PaymentBase = kbk.PaymentBase,
                DocNumber = "0",
                PaymentType = BudgetaryPaymentType.Other,
                Recipient = fundsRequisites,
                Description = request.Date >= BudgetaryPaymentDates.FormatDate16052025
                    ? "ЕНП. НДС не облагается"
                    : "Единый налоговый платеж. НДС не облагается"
            };
        }

        private async Task<BudgetaryRecipient> ResolveOrderDetailsForDefaultFieldsAsync()
        {
            var requisites = await recipientRequisitesReader.GetUnifiedBudgetaryPaymentFnsDepartmentRequisitesAsync();

            return new BudgetaryRecipient
            {
                Name = requisites?.Name,
                Inn = requisites?.Inn,
                Kpp = requisites?.Kpp,
                SettlementAccount = requisites?.SettlementAccount,
                BankBik = requisites?.BankBik,
                BankName = requisites?.BankName,
                UnifiedSettlementAccount = requisites?.UnifiedSettlementAccount,
                Oktmo = "0"
            };
        }
    }
}
