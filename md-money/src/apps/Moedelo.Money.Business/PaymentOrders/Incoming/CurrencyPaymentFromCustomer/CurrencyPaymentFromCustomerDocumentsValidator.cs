using Moedelo.Docs.ApiClient.Abstractions.SalesCurrencyInvoices;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [InjectAsSingleton(typeof(CurrencyPaymentFromCustomerDocumentsValidator))]
    internal class CurrencyPaymentFromCustomerDocumentsValidator
    {
        private static readonly IReadOnlyCollection<LinkedDocumentType> RestrictTypes = new[]
        {
            LinkedDocumentType.SalesCurrencyInvoice
        };

        private readonly IBaseDocumentReader baseDocumentReader;
        private readonly LinkedDocumentPaidSumReader paidSumReader;
        private readonly ISalesCurrencyInvoicesApiClient currencyInvoicesApiClient;

        public CurrencyPaymentFromCustomerDocumentsValidator(
            IBaseDocumentReader baseDocumentReader,
            LinkedDocumentPaidSumReader paidSumReader,
            ISalesCurrencyInvoicesApiClient currencyInvoicesApiClient)
        {
            this.baseDocumentReader = baseDocumentReader;
            this.paidSumReader = paidSumReader;
            this.currencyInvoicesApiClient = currencyInvoicesApiClient;
        }

        /// <summary>
        /// Валидирует ссылки на документы
        /// </summary>
        public async Task ValidateAsync(CurrencyPaymentFromCustomerSaveRequest request)
        {
            var documentLinks = request.LinkedDocuments;
            if (documentLinks?.Any() != true)
            {
                return;
            }
            var documentBaseIds = documentLinks.Select(x => x.DocumentBaseId).ToArray();
            await DocumentLinksDuplicateValidator.Validate(documentBaseIds);

            var baseDocuments = await baseDocumentReader.GetByIdsAsync(documentBaseIds);
            var paidSums = await paidSumReader.GetPaidSumsForIncomingPaymentAsync(request.DocumentBaseId, baseDocuments, RestrictTypes);
            var currencyInvoices = await currencyInvoicesApiClient.GetByBaseIdsAsync(documentBaseIds);

            var i = 0;
            foreach (var link in documentLinks)
            {
                var document = currencyInvoices.FirstOrDefault(x => x.DocumentBaseId == link.DocumentBaseId);
                if (document == null)
                {
                    throw new BusinessValidationException($"Documents[{i}].DocumentBaseId", $"Не найден документ #{link.DocumentBaseId}");
                }

                if (document.KontragentId != request.Kontragent?.Id)
                {
                    throw new BusinessValidationException($"Documents[{i}].DocumentBaseId",
                        $"Контрагент в документе #{link.DocumentBaseId} не совпадает с контрагентом в платеже");
                }

                if (request.SettlementAccountId != document.SettlementAccountId)
                {
                    throw new BusinessValidationException($"Documents[{i}].DocumentBaseId", $"Р/сч в документе #{link.DocumentBaseId} не совпадает с р/сч в платеже");
                }

                paidSums.TryGetValue(link.DocumentBaseId, out var paidSum);
                var accessibleSum = document.Sum - paidSum;
                if (accessibleSum < link.LinkSum)
                {
                    throw new BusinessValidationException($"Documents[{i}].Sum", $"Сумма документа #{link.DocumentBaseId} доступная к оплате меньше, чем сумма указаная к оплате");
                }
                i++;
            }
        }
    }
}