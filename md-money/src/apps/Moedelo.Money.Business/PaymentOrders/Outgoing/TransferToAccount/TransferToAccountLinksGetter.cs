using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton(typeof(TransferToAccountLinksGetter))]
    internal sealed class TransferToAccountLinksGetter
    {
        private readonly ILogger logger;
        private readonly ILinksReader linksReader;

        public TransferToAccountLinksGetter(
            ILogger<TransferToAccountLinksGetter> logger,
            ILinksReader linksReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
        }

        public async Task<TransferToAccountLinks> GetAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId).ConfigureAwait(false);
                return new TransferToAccountLinks { TransferFromAccount = GetPaymentOrder(links) };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new TransferToAccountLinks
                {
                    TransferFromAccount = new RemoteServiceResponse<PaymentOrderLink> { Status = RemoteServiceStatus.Error },
                };
            }
        }

        private RemoteServiceResponse<PaymentOrderLink> GetPaymentOrder(IReadOnlyCollection<LinkWithDocument> links)
        {
            var paymentOrderLink = links.FirstOrDefault(x => x.Document.Type == LinkedDocumentType.PaymentOrder);
            return new RemoteServiceResponse<PaymentOrderLink>
            {
                Data = paymentOrderLink != null
                    ? Map(paymentOrderLink)
                    : null,
                Status = RemoteServiceStatus.Ok
            };
        }

        private static PaymentOrderLink Map(LinkWithDocument paymentOrderLink)
        {
            return new PaymentOrderLink
            {
                DocumentBaseId = paymentOrderLink.Document.Id,
                Date = paymentOrderLink.Document.Date,
                Number = paymentOrderLink.Document.Number,
                Sum = paymentOrderLink.Document.Sum
            };
        }
    }
}