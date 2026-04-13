using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Contracts;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(RentPaymentLinksGetter))]
    internal sealed class RentPaymentLinksGetter
    {
        private readonly ILogger logger;
        private readonly ILinksReader linksReader;
        private readonly InventoryCardReader inventoryCardReader;
        private readonly IContractsReader contractsReader;

        public RentPaymentLinksGetter(
            ILogger<RentPaymentLinksGetter> logger,
            ILinksReader linksReader,
            InventoryCardReader inventoryCardReader,
            IContractsReader contractsReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
            this.inventoryCardReader = inventoryCardReader;
            this.contractsReader = contractsReader;
        }

        public async Task<RentPaymentLinks> GetAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId);
                var inventoryCard = await GetInventoryCard(links);
                var contract = await GetContractAsync(links).ConfigureAwait(false);

                return new RentPaymentLinks
                {
                    Contract = contract,
                    InventoryCard = inventoryCard
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new RentPaymentLinks
                {
                    Contract = new RemoteServiceResponse<ContractLink> { Status = RemoteServiceStatus.Error },
                    InventoryCard = new RemoteServiceResponse<InventoryCard> { Status = RemoteServiceStatus.Error }
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

        private async Task<RemoteServiceResponse<InventoryCard>> GetInventoryCard(IReadOnlyCollection<LinkWithDocument> links)
        {
            var inventoryCardLink = links.FirstOrDefault(x => x.Document.Type == LinkedDocumentType.InventoryCard);

            if (inventoryCardLink == null)
            {
                return new RemoteServiceResponse<InventoryCard> { Status = RemoteServiceStatus.Ok };
            }

            InventoryCard inventoryCard;

            // получим данные инвентарной карты, которых нет в связи
            try
            {
                inventoryCard = await inventoryCardReader.GetByBaseIdAsync(inventoryCardLink.Document.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new RemoteServiceResponse<InventoryCard> { Status = RemoteServiceStatus.Error };
            }

            if (inventoryCard == null)
            {
                return new RemoteServiceResponse<InventoryCard> { Status = RemoteServiceStatus.Ok };
            }

            return new RemoteServiceResponse<InventoryCard>
            {
                Status = RemoteServiceStatus.Ok,
                Data = new InventoryCard
                {
                    DocumentBaseId = inventoryCard.DocumentBaseId,
                    FixedAssetName = inventoryCard.FixedAssetName,
                    InventoryNumber = inventoryCard.InventoryNumber
                }
            };
        }
    }
}
