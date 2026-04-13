using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Contracts;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [InjectAsSingleton(typeof(CurrencyPaymentFromCustomerLinksGetter))]
    internal sealed class CurrencyPaymentFromCustomerLinksGetter
    {
        private readonly ILogger logger;
        private readonly ILinksReader linksReader;
        private readonly IContractsReader contractsReader;
        private readonly LinkedDocumentPaidSumReader paidSumReader;

        public CurrencyPaymentFromCustomerLinksGetter(
            ILogger<CurrencyPaymentFromCustomerLinksGetter> logger,
            ILinksReader linksReader,
            IContractsReader contractsReader,
            LinkedDocumentPaidSumReader paidSumReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
            this.contractsReader = contractsReader;
            this.paidSumReader = paidSumReader;
        }

        public async Task<CurrencyPaymentFromCustomerLinks> GetAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId);
                var contract = await GetContractAsync(links);
                var salesCurrencyInvoicesLinks = links.Where(link => link.Document.Type == LinkedDocumentType.SalesCurrencyInvoice).ToArray();
                var baseDocs = salesCurrencyInvoicesLinks.Select(link => link.Document).ToArray();

                var paidSums = await paidSumReader.GetPaidSumsForIncomingPaymentAsync(
                    documentBaseId,
                    baseDocs,
                    new[] { LinkedDocumentType.SalesCurrencyInvoice });

                return new CurrencyPaymentFromCustomerLinks
                {
                    Contract = contract,
                    Documents = GetDocuments(salesCurrencyInvoicesLinks, paidSums)
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new CurrencyPaymentFromCustomerLinks
                {
                    Contract = new RemoteServiceResponse<ContractLink> { Status = RemoteServiceStatus.Error },
                    Documents = new RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> { Status = RemoteServiceStatus.Error }
                };
            }
        }

        private RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> GetDocuments(LinkWithDocument[] links, IReadOnlyDictionary<long, decimal> paidSums)
        {
            return new RemoteServiceResponse<IReadOnlyCollection<DocumentLink>>
            {
                Data = links.Length > 0 ? links.Select(dl => MapToDocumentLink(dl, paidSums)).ToArray() : null,
                Status = RemoteServiceStatus.Ok
            };
        }

        private async Task<RemoteServiceResponse<ContractLink>> GetContractAsync(IReadOnlyCollection<LinkWithDocument> links)
        {
            var contractLink = links.FirstOrDefault(x =>
                x.Document.Type == LinkedDocumentType.Project ||
                x.Document.Type == LinkedDocumentType.MainContract);

            if (contractLink == null)
            {
                return new RemoteServiceResponse<ContractLink> { Status = RemoteServiceStatus.Ok };
            }

            var contract = await contractsReader.GetByBaseIdAsync(contractLink.Document.Id).ConfigureAwait(false);

            return new RemoteServiceResponse<ContractLink>
            {
                Data = new ContractLink
                {
                    DocumentBaseId = contractLink.Document.Id,
                    Date = contractLink.Document.Date,
                    Number = contractLink.Document.Number,
                    ContractKind = contract.ContractKind
                },
                Status = RemoteServiceStatus.Ok
            };
        }

        private static DocumentLink MapToDocumentLink(LinkWithDocument documentLink, IReadOnlyDictionary<long, decimal> paidSums)
        {
            // если в словаре нет значения, это может говорить о том, что документ уже удален (очереди запаздывают с обработкой)
            var paidSum = paidSums.GetValueOrDefault(documentLink.Document.Id, 0);
            return new DocumentLink
            {
                DocumentBaseId = documentLink.Document.Id,
                Type = (DocumentType)documentLink.Document.Type,
                Date = documentLink.Document.Date,
                Number = documentLink.Document.Number,
                DocumentSum = documentLink.Document.Sum,
                LinkSum = documentLink.Sum,
                PaidSum = paidSum
            };
        }
    }
}