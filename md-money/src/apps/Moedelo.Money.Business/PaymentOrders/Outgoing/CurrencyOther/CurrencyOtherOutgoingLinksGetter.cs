using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Contracts;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther
{
    [InjectAsSingleton(typeof(CurrencyOtherOutgoingLinksGetter))]
    internal sealed class CurrencyOtherOutgoingLinksGetter
    {
        private readonly ILogger logger;
        private readonly ILinksReader linksReader;
        private readonly IContractsReader contractsReader;

        public CurrencyOtherOutgoingLinksGetter(
            ILogger<CurrencyOtherOutgoingLinksGetter> logger,
            ILinksReader linksReader,
            IContractsReader contractsReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
            this.contractsReader = contractsReader;
        }

        public async Task<CurrencyOtherOutgoingLinks> GetAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId).ConfigureAwait(false);
                var contract = await GetContractAsync(links).ConfigureAwait(false);

                return new CurrencyOtherOutgoingLinks
                {
                    Contract = contract
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new CurrencyOtherOutgoingLinks
                {
                    Contract = new RemoteServiceResponse<ContractLink> { Status = RemoteServiceStatus.Error }
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
    }
}