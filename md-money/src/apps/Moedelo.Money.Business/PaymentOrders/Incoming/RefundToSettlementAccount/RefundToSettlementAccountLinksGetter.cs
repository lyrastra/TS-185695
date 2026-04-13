using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Contracts;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount
{
    [InjectAsSingleton(typeof(RefundToSettlementAccountLinksGetter))]
    internal sealed class RefundToSettlementAccountLinksGetter
    {
        private readonly ILogger logger;
        private readonly ILinksReader linksReader;
        private readonly IContractsReader contractsReader;

        public RefundToSettlementAccountLinksGetter(
            ILogger<RefundToSettlementAccountLinksGetter> logger,
            ILinksReader linksReader,
            IContractsReader contractsReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
            this.contractsReader = contractsReader;
        }

        public async Task<RefundToSettlementAccountLinks> GetAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId).ConfigureAwait(false);
                var contract = await GetContractAsync(links).ConfigureAwait(false);

                return new RefundToSettlementAccountLinks
                {
                    Bills = GetBillLinks(links),
                    Contract = contract
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new RefundToSettlementAccountLinks
                {
                    Bills = new RemoteServiceResponse<IReadOnlyCollection<BillLink>> { Status = RemoteServiceStatus.Error },
                    Contract = new RemoteServiceResponse<ContractLink> { Status = RemoteServiceStatus.Error }
                };
            }
        }

        private static RemoteServiceResponse<IReadOnlyCollection<BillLink>> GetBillLinks(IReadOnlyCollection<LinkWithDocument> links)
        {
            var bills = links.Where(x => x.Document.Type == LinkedDocumentType.Bill)
                .Select(x => new BillLink
                {
                    DocumentBaseId = x.Document.Id,
                    Date = x.Document.Date,
                    Number = x.Document.Number,
                    BillSum = x.Document.Sum,
                    LinkSum = x.Sum
                }).ToList();
            return new RemoteServiceResponse<IReadOnlyCollection<BillLink>>
            {
                Data = bills.Any() ? bills : null,
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
    }
}