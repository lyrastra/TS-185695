using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Contracts;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(PaymentFromCustomerLinksGetter))]
    internal sealed class PaymentFromCustomerLinksGetter
    {
        private static readonly LinkedDocumentType[] types = new[]
        {
            LinkedDocumentType.Bill,
            LinkedDocumentType.Waybill,
            LinkedDocumentType.Statement,
            LinkedDocumentType.SalesUpd,
        };

        private readonly ILogger logger;
        private readonly LinkedDocumentPaidSumReader paidSumReader;
        private readonly ILinksReader linksReader;
        private readonly IContractsReader contractsReader;

        public PaymentFromCustomerLinksGetter(
            ILogger<PaymentFromCustomerLinksGetter> logger,
            LinkedDocumentPaidSumReader paidSumReader,
            ILinksReader linksReader,
            IContractsReader contractsReader)
        {
            this.logger = logger;
            this.paidSumReader = paidSumReader;
            this.linksReader = linksReader;
            this.contractsReader = contractsReader;
        }

        public async Task<PaymentFromCustomerLinks> GetAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId);

                var baseDocuments = links.Where(x => types.Contains(x.Document.Type))
                    .Select(x => x.Document)
                    .ToArray();

                var paidSums = await paidSumReader.GetPaidSumsForIncomingPaymentAsync(documentBaseId, baseDocuments, types);
                var contract = await GetContractLinkAsync(links);

                return new PaymentFromCustomerLinks
                {
                    Contract = RemoteServiceResponse.Ok(contract),
                    Bills = RemoteServiceResponse.Ok(GetBillLinks(links, paidSums)),
                    Documents = RemoteServiceResponse.Ok(GetDocumentLinks(links, paidSums)),
                    Invoices = RemoteServiceResponse.Ok(GetInvoiceLinks(links)),
                    ReserveSum = RemoteServiceResponse.Ok(GetReserveSum(links))
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new PaymentFromCustomerLinks
                {
                    Bills = RemoteServiceResponse.Error<IReadOnlyCollection<BillLink>>(),
                    Contract = RemoteServiceResponse.Error<ContractLink>(),
                    Documents = RemoteServiceResponse.Error<IReadOnlyCollection<DocumentLink>>(),
                    Invoices = RemoteServiceResponse.Error<IReadOnlyCollection<InvoiceLink>>(),
                    ReserveSum = RemoteServiceResponse.Error<decimal?>()
                };
            }
        }

        public async Task<IReadOnlyDictionary<long, PaymentFromCustomerLinks>> GetAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            try
            {
                var linksByPaymentMap = await linksReader.GetLinksWithDocumentsAsync(documentBaseIds);
                var paidSumsByPaymentMap = await paidSumReader.GetPaidSumsForIncomingPaymentAsync(linksByPaymentMap, types);
                var contractLinkByPaymentMap = await GetContractLinksAsync(linksByPaymentMap);
                var billLinksByPaymentMap = GetBillLinks(linksByPaymentMap, paidSumsByPaymentMap);
                var documentLinksByPaymentMap = GetDocumentLinks(linksByPaymentMap, paidSumsByPaymentMap);
                var invoiceLinksByPaymentMap = GetInvoiceLinks(linksByPaymentMap);
                var reserveSumByPaymentMap = GetReserveSum(linksByPaymentMap);

                return documentBaseIds.ToDictionary(x => x, x =>
                    new PaymentFromCustomerLinks
                    {
                        Contract = RemoteServiceResponse.Ok(contractLinkByPaymentMap.GetValueOrDefault(x)),
                        Bills = RemoteServiceResponse.Ok(billLinksByPaymentMap.GetValueOrDefault(x)),
                        Documents = RemoteServiceResponse.Ok(documentLinksByPaymentMap.GetValueOrDefault(x)),
                        Invoices = RemoteServiceResponse.Ok(invoiceLinksByPaymentMap.GetValueOrDefault(x)),
                        ReserveSum = RemoteServiceResponse.Ok(reserveSumByPaymentMap.GetValueOrDefault(x))
                    });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return documentBaseIds.ToDictionary(
                x => x,
                x => new PaymentFromCustomerLinks
                {
                    Contract = RemoteServiceResponse.Error<ContractLink>(),
                    Bills = RemoteServiceResponse.Error<IReadOnlyCollection<BillLink>>(),
                    Documents = RemoteServiceResponse.Error<IReadOnlyCollection<DocumentLink>>(),
                    Invoices = RemoteServiceResponse.Error<IReadOnlyCollection<InvoiceLink>>(),
                    ReserveSum = RemoteServiceResponse.Error<decimal?>()
                });
            }
        }

        public async Task<RemoteServiceResponse<IReadOnlyCollection<InvoiceLink>>> GetInvoicesLinkAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId);

                return RemoteServiceResponse.Ok(GetInvoiceLinks(links));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return RemoteServiceResponse.Error<IReadOnlyCollection<InvoiceLink>>();
            }
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

        private static IReadOnlyCollection<BillLink> GetBillLinks(
            IReadOnlyCollection<LinkWithDocument> links,
            IReadOnlyDictionary<long, decimal> paidSums)
        {
            var billLinks = links.Where(x => x.Document.Type == LinkedDocumentType.Bill)
                .GroupJoin(paidSums, x => x.Document.Id, x => x.Key, MapToBillLink)
                .ToArray();
            var billLinksResponse = billLinks.Length > 0
                ? billLinks
                : null;

            return billLinksResponse;
        }

        private static IReadOnlyDictionary<long, IReadOnlyCollection<BillLink>> GetBillLinks(
            IReadOnlyDictionary<long, LinkWithDocument[]> linksByPaymentMap,
            IReadOnlyDictionary<long, IReadOnlyDictionary<long, decimal>> paidSumsByPaymentMap)
        {
            return linksByPaymentMap.ToDictionary(
                x => x.Key,
                x => GetBillLinks(
                    x.Value,
                    paidSumsByPaymentMap.GetValueOrDefault(x.Key) ?? new Dictionary<long, decimal>()));
        }

        private static IReadOnlyCollection<DocumentLink> GetDocumentLinks(
            IReadOnlyCollection<LinkWithDocument> links,
            IReadOnlyDictionary<long, decimal> paidSums)
        {
            var documentLinks = links.Where(x =>
                    x.Document.Type == LinkedDocumentType.Waybill ||
                    x.Document.Type == LinkedDocumentType.Statement ||
                    x.Document.Type == LinkedDocumentType.SalesUpd)
                .GroupJoin(paidSums, x => x.Document.Id, x => x.Key, MapToDocumentLink)
                .ToArray();
            var documentLinksResponse = documentLinks.Length > 0
                ? documentLinks
                : null;

            return documentLinksResponse;
        }

        private static IReadOnlyDictionary<long, IReadOnlyCollection<DocumentLink>> GetDocumentLinks(
            IReadOnlyDictionary<long, LinkWithDocument[]> linksByPaymentMap,
            IReadOnlyDictionary<long, IReadOnlyDictionary<long, decimal>> paidSumsByPaymentMap)
        {
            return linksByPaymentMap.ToDictionary(
                x => x.Key,
                x => GetDocumentLinks(
                    x.Value,
                    paidSumsByPaymentMap.GetValueOrDefault(x.Key) ?? new Dictionary<long, decimal>()));
        }

        private static IReadOnlyCollection<InvoiceLink> GetInvoiceLinks(IReadOnlyCollection<LinkWithDocument> links)
        {
            var documentLinks = links.Where(x => x.Document.Type == LinkedDocumentType.Invoice)
                .Select(MapToInvoiceLink)
                .ToArray();

            return documentLinks.Length > 0
                ? documentLinks
                : null;
        }

        private static IReadOnlyDictionary<long, IReadOnlyCollection<InvoiceLink>> GetInvoiceLinks(
            IReadOnlyDictionary<long, LinkWithDocument[]> linksByPaymentMap)
        {
            return linksByPaymentMap.ToDictionary(
                x => x.Key,
                x => GetInvoiceLinks(x.Value));
        }

        private static decimal? GetReserveSum(IReadOnlyCollection<LinkWithDocument> links)
        {
            // берем сумму связи
            return links.FirstOrDefault(x => x.Document.Type == LinkedDocumentType.PaymentReserve)?.Sum;
        }

        private static IReadOnlyDictionary<long, decimal?> GetReserveSum(
            IReadOnlyDictionary<long, LinkWithDocument[]> linksByPaymentMap)
        {
            return linksByPaymentMap.ToDictionary(
                x => x.Key,
                x => GetReserveSum(x.Value));
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

        private static InvoiceLink MapToInvoiceLink(LinkWithDocument documentLink)
        {
            return new InvoiceLink
            {
                DocumentBaseId = documentLink.Document.Id,
                Date = documentLink.Document.Date,
                Number = documentLink.Document.Number,
            };
        }
    }
}