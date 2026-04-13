using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Contracts;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer.Links
{
    [InjectAsSingleton(typeof(MediationFeeLinksGetter))]
    internal sealed class MediationFeeLinksGetter
    {
        private static readonly LinkedDocumentType[] types = new[]
        {
            LinkedDocumentType.Bill,
            LinkedDocumentType.Waybill,
            LinkedDocumentType.Statement,
            LinkedDocumentType.SalesUpd,
            LinkedDocumentType.MiddlemanReport
        };

        private readonly ILogger logger;
        private readonly ILinksReader linksReader;
        private readonly LinkedDocumentPaidSumReader paidSumReader;
        private readonly IContractsReader contractsReader;

        public MediationFeeLinksGetter(
            ILogger<MediationFeeLinksGetter> logger,
            ILinksReader linksReader,
            LinkedDocumentPaidSumReader paidSumReader,
            IContractsReader contractsReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
            this.paidSumReader = paidSumReader;
            this.contractsReader = contractsReader;
        }

        public async Task<MediationFeeLinks> GetAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId);

                var documentBaseIds = links.Where(x => types.Contains(x.Document.Type))
                    .Select(x => x.Document.Id)
                    .ToArray();

                var paidSums = await paidSumReader.GetPaidSumsForMediationFeeAsync(documentBaseId, documentBaseIds, types);
                var contract = await GetContractAsync(links);

                return new MediationFeeLinks
                {
                    Bills = GetBillLinks(links, paidSums),
                    Documents = GetDocumentLinks(links, paidSums),
                    Contract = contract
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new MediationFeeLinks
                {
                    Bills = new RemoteServiceResponse<IReadOnlyCollection<BillLink>> { Status = RemoteServiceStatus.Error },
                    Contract = new RemoteServiceResponse<ContractLink> { Status = RemoteServiceStatus.Error },
                    Documents = new RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> { Status = RemoteServiceStatus.Error }
                };
            }
        }

        private static RemoteServiceResponse<IReadOnlyCollection<BillLink>> GetBillLinks(IReadOnlyCollection<LinkWithDocument> links, IDictionary<long, decimal> paidSums)
        {
            var billLinks = links.Where(x => x.Document.Type == LinkedDocumentType.Bill)
                .GroupJoin(paidSums, x => x.Document.Id, x => x.Key, MapToBillLink)
                .ToArray();

            return new RemoteServiceResponse<IReadOnlyCollection<BillLink>>
            {
                Data = billLinks.Length > 0 ? billLinks : null,
                Status = RemoteServiceStatus.Ok
            };
        }

        private RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> GetDocumentLinks(IReadOnlyCollection<LinkWithDocument> links, IDictionary<long, decimal> paidSums)
        {
            var documentLinks = links.Where(x =>
                    x.Document.Type == LinkedDocumentType.Waybill ||
                    x.Document.Type == LinkedDocumentType.Statement ||
                    x.Document.Type == LinkedDocumentType.SalesUpd ||
                    x.Document.Type == LinkedDocumentType.MiddlemanReport)
                .GroupJoin(paidSums, x => x.Document.Id, x => x.Key, MapToDocumentLink)
                .ToArray();

            return new RemoteServiceResponse<IReadOnlyCollection<DocumentLink>>
            {
                Data = documentLinks.Length > 0 ? documentLinks : null,
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

            var contract = await contractsReader.GetByBaseIdAsync(contractLink.Document.Id);

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

        private static BillLink MapToBillLink(LinkWithDocument billLink, IEnumerable<KeyValuePair<long, decimal>> paidSums)
        {
            var paidSum = paidSums.FirstOrDefault();
            return new BillLink
            {
                DocumentBaseId = billLink.Document.Id,
                Date = billLink.Document.Date,
                Number = billLink.Document.Number,
                BillSum = billLink.Document.Sum,
                LinkSum = billLink.Sum,
                PaidSum = paidSum.Value
            };
        }

        private static DocumentLink MapToDocumentLink(LinkWithDocument documentLink, IEnumerable<KeyValuePair<long, decimal>> paidSums)
        {
            var paidSum = paidSums.FirstOrDefault();
            return new DocumentLink
            {
                DocumentBaseId = documentLink.Document.Id,
                Type = (DocumentType)documentLink.Document.Type,
                Date = documentLink.Document.Date,
                Number = documentLink.Document.Number,
                DocumentSum = documentLink.Document.Sum,
                LinkSum = documentLink.Sum,
                PaidSum = paidSum.Value
            };
        }
    }
}