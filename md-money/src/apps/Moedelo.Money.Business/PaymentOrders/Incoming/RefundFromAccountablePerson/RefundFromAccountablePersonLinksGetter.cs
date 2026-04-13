using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [InjectAsSingleton(typeof(RefundFromAccountablePersonLinksGetter))]
    internal sealed class RefundFromAccountablePersonLinksGetter
    {
        private readonly ILogger logger;
        private readonly ILinksReader linksReader;

        public RefundFromAccountablePersonLinksGetter(
            ILogger<RefundFromAccountablePersonLinksGetter> logger,
            ILinksReader linksReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
        }

        public async Task<RefundFromAccountablePersonLinks> GetAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId).ConfigureAwait(false);
                return new RefundFromAccountablePersonLinks
                {
                    AdvanceStatement = new RemoteServiceResponse<AdvanceStatementLink>
                    {
                        Data = GetAdvanceStatement(links),
                        Status = RemoteServiceStatus.Ok
                    }
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new RefundFromAccountablePersonLinks
                {
                    AdvanceStatement = new RemoteServiceResponse<AdvanceStatementLink> { Status = RemoteServiceStatus.Error }
                };
            }
        }

        private static AdvanceStatementLink GetAdvanceStatement(IReadOnlyCollection<LinkWithDocument> links)
        {
            var advanceStatementLink = links.FirstOrDefault(x => x.Document.Type == LinkedDocumentType.AdvanceStatement);
            if (advanceStatementLink == null)
            {
                return null;
            }

            return new AdvanceStatementLink
            {
                DocumentBaseId = advanceStatementLink.Document.Id,
                Date = advanceStatementLink.Document.Date,
                Number = advanceStatementLink.Document.Number
            };
        }
    }
}