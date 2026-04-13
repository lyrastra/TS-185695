using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment.CurrencyInvoiceNdsPayments;
using Moedelo.Money.Enums;
using LinkedDocumentType = Moedelo.LinkedDocuments.Enums.LinkedDocumentType;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(BudgetaryPaymentLinksGetter))]
    internal class BudgetaryPaymentLinksGetter
    {
        private readonly ILogger logger;
        private readonly ILinksClient linksClient;

        public BudgetaryPaymentLinksGetter(
            ILogger<BudgetaryPaymentLinksGetter> logger,
            ILinksClient linksClient)
        {
            this.logger = logger;
            this.linksClient = linksClient;
        }

        public async Task<BudgetaryPaymentLinks> GetAsync(BudgetaryPaymentResponse model)
        {
            // БП могут быть связаны только с валютными инвойсами, и т. к. это редкий случай, будем запрашивать их не всегда
            if (model.AccountCode != BudgetaryAccountCodes.Nds ||
                model.KbkPaymentType != KbkPaymentType.Payment)
            {
                return new BudgetaryPaymentLinks
                {
                    CurrencyInvoices = new RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>>
                    {
                        Data = Array.Empty<CurrencyInvoiceLink>(),
                        Status = RemoteServiceStatus.Ok
                    }
                };
            }

            try
            {
                var links = await linksClient.GetLinksWithDocumentsAsync(model.DocumentBaseId);

                return new BudgetaryPaymentLinks
                {
                    CurrencyInvoices = GetCurrencyInvoicesLinks(links)
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new BudgetaryPaymentLinks
                {
                    CurrencyInvoices = new RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>> { Status = RemoteServiceStatus.Error }
                };
            }
        }

        private RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>> GetCurrencyInvoicesLinks(IReadOnlyCollection<LinkWithDocumentDto> links)
        {
            var documents = links.Where(x =>
                    x.Document.Type == LinkedDocumentType.PurchaseCurrencyInvoice)
                .Select(x => new CurrencyInvoiceLink
                {
                    DocumentBaseId = x.Document.Id,
                    DocumentDate = x.Document.Date,
                    DocumentNumber = x.Document.Number,
                    LinkSum = x.Sum
                }).ToList();

            return new RemoteServiceResponse<IReadOnlyCollection<CurrencyInvoiceLink>>
            {
                Data = documents,
                Status = RemoteServiceStatus.Ok
            };
        }
    }
}