using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Contracts;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton(typeof(CurrencyPaymentToSupplierLinksGetter))]
    internal sealed class CurrencyPaymentToSupplierLinksGetter
    {
        private readonly IReadOnlyCollection<LinkedDocumentType> primaryDocTypes = new []
        {
            LinkedDocumentType.PurchaseCurrencyInvoice
        };
        
        private readonly ILogger logger;
        private readonly ILinksReader linksReader;
        private readonly IContractsReader contractsReader;
        private readonly LinkedDocumentPaidSumReader paidSumReader;

        public CurrencyPaymentToSupplierLinksGetter(
            ILogger<CurrencyPaymentToSupplierLinksGetter> logger,
            ILinksReader linksReader,
            IContractsReader contractsReader, 
            LinkedDocumentPaidSumReader paidSumReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
            this.contractsReader = contractsReader;
            this.paidSumReader = paidSumReader;
        }

        public async Task<CurrencyPaymentToSupplierLinks> GetAsync(long paymentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(paymentBaseId);

                return new CurrencyPaymentToSupplierLinks
                {
                    Contract = await GetContractAsync(links),
                    Documents = await GetDocumentsAsync(paymentBaseId, links)
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new CurrencyPaymentToSupplierLinks
                {
                    Contract = new RemoteServiceResponse<ContractLink> { Status = RemoteServiceStatus.Error },
                };
            }
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

        private async Task<RemoteServiceResponse<IReadOnlyCollection<DocumentLink>>> GetDocumentsAsync(
            long paymentBaseId,
            IReadOnlyCollection<LinkWithDocument> links)
        {
            var linksWithDocs = links?
                .Where(x => primaryDocTypes.Contains(x.Document.Type))
                .ToArray();
            
            if (linksWithDocs?.Any() != true)
            {
                return new RemoteServiceResponse<IReadOnlyCollection<DocumentLink>>
                {
                    Status = RemoteServiceStatus.Ok
                }; 
            }

            var paidSums = await paidSumReader.GetPaidSumsForOutgoingPaymentAsync(
                paymentBaseId,
                linksWithDocs.Select(x => x.Document).ToArray(),
                primaryDocTypes
            );

            var documents = linksWithDocs
                .Select(x => new DocumentLink
                {
                    DocumentBaseId = x.Document.Id,
                    Type = (DocumentType)x.Document.Type,
                    Date = x.Document.Date,
                    Number = x.Document.Number,
                    DocumentSum = x.Document.Sum,
                    LinkSum = x.Sum,
                    PaidSum = paidSums.GetValueOrDefault(x.Document.Id)
                }).ToArray();

            return new RemoteServiceResponse<IReadOnlyCollection<DocumentLink>>
            {
                Data = documents,
                Status = RemoteServiceStatus.Ok
            };
        }
    }
}