using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Contracts;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other
{
    [InjectAsSingleton(typeof(OtherIncomingLinksGetter))]
    internal sealed class OtherIncomingLinksGetter
    {
        private readonly ILogger logger;
        private readonly ILinksReader linksReader;
        private readonly IContractsReader contractsReader;

        public OtherIncomingLinksGetter(
            ILogger<OtherIncomingLinksGetter> logger,
            ILinksReader linksReader,
            IContractsReader contractsReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
            this.contractsReader = contractsReader;
        }

        public async Task<OtherIncomingLinks> GetAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId);
                var contract = await GetContractLinkAsync(links);

                return new OtherIncomingLinks
                {
                    Bills = RemoteServiceResponse.Ok(GetBillLinks(links)),
                    Contract = RemoteServiceResponse.Ok(contract)
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new OtherIncomingLinks
                {
                    Bills = new RemoteServiceResponse<IReadOnlyCollection<BillLink>> { Status = RemoteServiceStatus.Error },
                    Contract = new RemoteServiceResponse<ContractLink> { Status = RemoteServiceStatus.Error }
                };
            }
        }

        public async Task<IReadOnlyDictionary<long, OtherIncomingLinks>> GetAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            try
            {
                var linksByPaymentMap = await linksReader.GetLinksWithDocumentsAsync(documentBaseIds);
                var contractLinkByPaymentMap = await GetContractLinksAsync(linksByPaymentMap);
                var billLinksByPaymentMap = GetBillLinks(linksByPaymentMap);

                return documentBaseIds.ToDictionary(x => x, x =>
                    new OtherIncomingLinks
                    {
                        Contract = RemoteServiceResponse.Ok(contractLinkByPaymentMap.GetValueOrDefault(x)),
                        Bills = RemoteServiceResponse.Ok(billLinksByPaymentMap.GetValueOrDefault(x)),
                    });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return documentBaseIds.ToDictionary(
                x => x,
                x => new OtherIncomingLinks
                {
                    Contract = RemoteServiceResponse.Error<ContractLink>(),
                    Bills = RemoteServiceResponse.Error<IReadOnlyCollection<BillLink>>(),
                });
            }
        }

        private static IReadOnlyCollection<BillLink> GetBillLinks(IReadOnlyCollection<LinkWithDocument> links)
        {
            var billLinks = links.Where(x => x.Document.Type == LinkedDocumentType.Bill)
                .Select(MapToBillLink).ToArray();
            var billLinksResponse = billLinks.Length > 0
                ? billLinks
                : null;

            return billLinksResponse;
        }

        private static IReadOnlyDictionary<long, IReadOnlyCollection<BillLink>> GetBillLinks(
            IReadOnlyDictionary<long, LinkWithDocument[]> linksByPaymentMap)
        {
            return linksByPaymentMap.ToDictionary(
                x => x.Key,
                x => GetBillLinks(x.Value));
        }

        private async Task<ContractLink> GetContractLinkAsync(IReadOnlyCollection<LinkWithDocument> links)
        {
            var contractLink = links.FirstOrDefault(x =>
                x.Document.Type == LinkedDocumentType.Project ||
                x.Document.Type == LinkedDocumentType.MainContract);

            if (contractLink == null)
            {
                return null;
            }

            var contract = await contractsReader.GetByBaseIdAsync(contractLink.Document.Id);
            return MapContractLink(contractLink, contract);
        }

        private async Task<IReadOnlyDictionary<long, ContractLink>> GetContractLinksAsync(
            IReadOnlyDictionary<long, LinkWithDocument[]> linksByPaymentMap)
        {
            var contractsByPaymentMap = linksByPaymentMap
                .Where(x => x.Value.Any(d =>
                    d.Document.Type == LinkedDocumentType.Project ||
                    d.Document.Type == LinkedDocumentType.MainContract))
                .ToDictionary(x => x.Key, x => x.Value.FirstOrDefault(d =>
                    d.Document.Type == LinkedDocumentType.Project ||
                    d.Document.Type == LinkedDocumentType.MainContract));

            if (contractsByPaymentMap.Count == 0)
            {
                return new Dictionary<long, ContractLink>();
            }

            var contractBaseIds = contractsByPaymentMap.Values
                .Select(x => x.Document.Id)
                .ToArray();
            var contracts = await contractsReader.GetByBaseIdsAsync(contractBaseIds);
            var contractsMap = contracts.ToDictionary(x => x.DocumentBaseId);

            return contractsByPaymentMap
                .ToDictionary(
                x => x.Key,
                x => MapContractLink(x.Value, contractsMap[x.Value.Document.Id]));
        }

        private static BillLink MapToBillLink(LinkWithDocument billLink)
        {
            return new BillLink
            {
                DocumentBaseId = billLink.Document.Id,
                Date = billLink.Document.Date,
                Number = billLink.Document.Number,
                BillSum = billLink.Document.Sum,
                LinkSum = billLink.Sum
            };
        }

        private static ContractLink MapContractLink(LinkWithDocument contractLink, Contract contract)
        {
            return new ContractLink
            {
                DocumentBaseId = contractLink.Document.Id,
                Date = contractLink.Document.Date,
                Number = contractLink.Document.Number,
                ContractKind = contract.ContractKind
            };
        }
    }
}