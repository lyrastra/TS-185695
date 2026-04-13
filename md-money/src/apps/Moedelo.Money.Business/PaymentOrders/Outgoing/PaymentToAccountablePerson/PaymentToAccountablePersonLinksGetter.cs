using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.LinkedDocuments.Links.Models;
using Moedelo.Money.Domain;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(PaymentToAccountablePersonLinksGetter))]
    internal sealed class PaymentToAccountablePersonLinksGetter
    {
        private readonly ILogger logger;
        private readonly ILinksReader linksReader;

        public PaymentToAccountablePersonLinksGetter(
            ILogger<PaymentToAccountablePersonLinksGetter> logger,
            ILinksReader linksReader)
        {
            this.logger = logger;
            this.linksReader = linksReader;
        }

        public async Task<PaymentToAccountablePersonLinks> GetAsync(long documentBaseId)
        {
            try
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId).ConfigureAwait(false);

                return new PaymentToAccountablePersonLinks
                {
                    Documents = GetAdvanceStatementLinks(links)
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new PaymentToAccountablePersonLinks
                {
                    Documents = new RemoteServiceResponse<IReadOnlyCollection<AdvanceStatementLink>> { Status = RemoteServiceStatus.Error }
                };
            }
        }

        private static RemoteServiceResponse<IReadOnlyCollection<AdvanceStatementLink>> GetAdvanceStatementLinks(IReadOnlyCollection<LinkWithDocument> links)
        {
            var documents = links.Where(x => x.Document.Type == LinkedDocumentType.AdvanceStatement)
                .Select(x => new AdvanceStatementLink
                {
                    DocumentBaseId = x.Document.Id,
                    Date = x.Document.Date,
                    Number = x.Document.Number,
                    Sum = x.Sum
                }).ToList();

            return new RemoteServiceResponse<IReadOnlyCollection<AdvanceStatementLink>>
            {
                Data = documents.Any() ? documents : null,
                Status = RemoteServiceStatus.Ok
            };
        }
    }
}