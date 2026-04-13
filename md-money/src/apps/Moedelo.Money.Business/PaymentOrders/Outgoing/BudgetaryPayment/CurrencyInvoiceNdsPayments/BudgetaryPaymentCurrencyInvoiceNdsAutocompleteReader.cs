using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Business.Kbks;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Enums;
using Moedelo.Money.PaymentOrders.Dto.PaymentOrders.Outgoing.BudgetaryPayment;
using LinkedDocumentType = Moedelo.LinkedDocuments.Enums.LinkedDocumentType;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentCurrencyInvoiceNdsAutocompleteReader))]
    internal class BudgetaryPaymentCurrencyInvoiceNdsAutocompleteReader : IBudgetaryPaymentCurrencyInvoiceNdsAutocompleteReader
    {
        private readonly IKbkReader kbkReader;
        private readonly BudgetaryPaymentCurrencyInvoiceNdsApiClient apiClient;
        private readonly ILinksClient linksClient;

        public BudgetaryPaymentCurrencyInvoiceNdsAutocompleteReader(
            IKbkReader kbkReader,
            BudgetaryPaymentCurrencyInvoiceNdsApiClient apiClient,
            ILinksClient linksClient)
        {
            this.kbkReader = kbkReader;
            this.apiClient = apiClient;
            this.linksClient = linksClient;
        }

        public async Task<IReadOnlyCollection<CurrencyInvoiceNdsPaymentsAutocompleteResponse>> GetAsync(CurrencyInvoiceNdsPaymentsAutocompleteRequest request)
        {
            // 1. Определить КБК
            var kbkType = request.IsNdsPaidAtCustoms ? KbkType.NdsTaxOnCustomsHouse : KbkType.NdsTaxImportToFns;
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

            // 2. Запросить БП по параметрам
            int kbkId = kbks.Single(x => x.KbkType == kbkType).Id; // ожидается единственный КБК
            var payments = await apiClient.GetCurrencyInvoiceNdsPaymentsByAsync(new CurrencyInvoiceNdsPaymentsRequestDto
            {
                Offset = 0,
                Limit = int.MaxValue,
                QueryByNumber = request.Query,
                KbkIds = new[] { kbkId }
            });

            // 3. Запросить связи, отфильтровать платежи, покрытые документами
            var paymentBaseIds = payments.Select(p => p.DocumentBaseId).Distinct().ToArray();
            var links = await linksClient.GetLinksWithDocumentsAsync(paymentBaseIds);

            // 4. Сформировать результат: применить сортировку и ограничения
            return payments
                .Select(p =>
                {
                    var currentPaymentLinks = links.GetValueOrDefault(p.DocumentBaseId);
                    if (currentPaymentLinks == null)
                    {
                        return MapToResult(p, p.Sum);
                    }

                    var coveredSum = currentPaymentLinks
                        .Where(x => x.Document.Type == LinkedDocumentType.PurchaseCurrencyInvoice)
                        // исключаем переданный документ из связи
                        .Where(x => x.Document.Id != request.CurrencyInvoiceBaseId)
                        .Sum(x => x.Sum);

                    return MapToResult(p, p.Sum - coveredSum);
                })
                .Where(p => p.Sum > 0)
                .OrderBy(p => p.Number)
                .Take(request.Count)
                .ToArray();
        }
        private static CurrencyInvoiceNdsPaymentsAutocompleteResponse MapToResult(CurrencyInvoiceNdsPaymentResponse payment, decimal uncoveredSum)
        {
            return new CurrencyInvoiceNdsPaymentsAutocompleteResponse
            {
                Id = payment.DocumentBaseId,
                Date = payment.Date,
                Number = payment.Number,
                Sum = uncoveredSum
            };
        }
    }
}