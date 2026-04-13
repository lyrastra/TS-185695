using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(TransferFromCashLinksGetter))]
    internal sealed class TransferFromCashLinksGetter
    {
        private readonly ILogger logger;
        private readonly ILinksReader linksReader;

        public TransferFromCashLinksGetter(
            ILogger<TransferFromCashLinksGetter> logger,
            ILinksReader linksReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
        }

        public async Task<TransferFromCashLinks> GetByBaseIdAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId).ConfigureAwait(false);

                var linkedDoc = links?.FirstOrDefault(x => x.Document?.Type == LinkedDocumentType.OutcomingCashOrder)?.Document;
                var cashOrder = linkedDoc != null
                    ? new CashOrderLink
                    {
                        DocumentBaseId = linkedDoc.Id,
                        Number = linkedDoc.Number,
                        Date = linkedDoc.Date,
                        Sum = linkedDoc.Sum
                    }
                    : null;

                return new TransferFromCashLinks
                {
                    CashOrder = new RemoteServiceResponse<CashOrderLink>
                    {
                        Data = cashOrder,
                        Status = RemoteServiceStatus.Ok,
                    }
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new TransferFromCashLinks
                {
                    CashOrder = new RemoteServiceResponse<CashOrderLink>
                    {
                        Status = RemoteServiceStatus.Error
                    }
                };
            }
        }
    }
}