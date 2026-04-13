using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    [InjectAsSingleton(typeof(WithdrawalFromAccountLinksGetter))]
    internal sealed class WithdrawalFromAccountLinksGetter
    {
        private readonly ILogger logger;
        private readonly ILinksReader linksReader;

        public WithdrawalFromAccountLinksGetter(
            ILogger<WithdrawalFromAccountLinksGetter> logger,
            ILinksReader linksReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
        }

        public async Task<WithdrawalFromAccountLinks> GetAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId).ConfigureAwait(false);
                return new WithdrawalFromAccountLinks { CashOrder = GetCashOrder(links) };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new WithdrawalFromAccountLinks
                {
                    CashOrder = new RemoteServiceResponse<CashOrderLink> { Status = RemoteServiceStatus.Error },
                };
            }
        }

        private RemoteServiceResponse<CashOrderLink> GetCashOrder(IReadOnlyCollection<LinkWithDocument> links)
        {
            var cashOrderLink = links.FirstOrDefault(x => x.Document.Type == LinkedDocumentType.IncomingCashOrder);
            return new RemoteServiceResponse<CashOrderLink>
            {
                Data = cashOrderLink != null
                    ? Map(cashOrderLink)
                    : null,
                Status = RemoteServiceStatus.Ok
            };
        }

        private static CashOrderLink Map(LinkWithDocument paymentOrderLink)
        {
            return new CashOrderLink
            {
                DocumentBaseId = paymentOrderLink.Document.Id,
                Date = paymentOrderLink.Document.Date,
                Number = paymentOrderLink.Document.Number,
                Sum = paymentOrderLink.Document.Sum
            };
        }
    }
}