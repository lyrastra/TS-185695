using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Domain.SelfCostPayments;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.BudgetaryPayment;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.TaxSelfCost
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentTaxSelfCostReader))]
    internal class BudgetaryPaymentTaxSelfCostReader : IBudgetaryPaymentTaxSelfCostReader
    {
        private static readonly KbkType[] CurrencyInvoiceNdsKbkTypes = new[] { KbkType.NdsTaxOnCustomsHouse, KbkType.NdsTaxImportToFns };

        private readonly IKbkReader kbkReader;
        private readonly BudgetaryPaymentCurrencyInvoiceNdsApiClient apiClient;

        public BudgetaryPaymentTaxSelfCostReader(
            IKbkReader kbkReader,
            BudgetaryPaymentCurrencyInvoiceNdsApiClient apiClient)
        {
            this.kbkReader = kbkReader;
            this.apiClient = apiClient;
        }

        public async Task<IReadOnlyCollection<SelfCostPayment>> GetSelfCostPaymentsAsync(SelfCostPaymentRequest request)
        {
            var kbks = await kbkReader.GetKbkByAccountCodeAsync(
                new BudgetaryKbkRequest
                {
                    AccountCode = BudgetaryAccountCodes.Nds,
                    PaymentType = KbkPaymentType.Payment,
                    // фильтр по периоду и дате потенциально некорректный (пока нужные КБК не версионировались)
                    Period = new BudgetaryPeriod { Type = BudgetaryPeriodType.None },
                    Date = DateTime.Today
                },
                false); // пока функционал доступен только для ИП

            var kbkIds = kbks
                .Where(x => CurrencyInvoiceNdsKbkTypes.Contains(x.KbkType))
                .Select(x => x.Id)
                .ToArray();
            var payments = await apiClient.GetCurrencyInvoiceNdsPaymentsByAsync(new CurrencyInvoiceNdsPaymentsRequestDto
            {
                Offset = request.Offset,
                Limit = request.Limit,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                KbkIds = kbkIds
            });

            return payments
                .Select(MapToResult)
                .ToArray();
        }

        private static SelfCostPayment MapToResult(CurrencyInvoiceNdsPaymentResponse payment)
        {
            return new SelfCostPayment
            {
                DocumentBaseId = payment.DocumentBaseId,
                Date = payment.Date,
                Number = payment.Number,
                Sum = payment.Sum,
                Type = OperationType.BudgetaryPayment
            };
        }
    }
}