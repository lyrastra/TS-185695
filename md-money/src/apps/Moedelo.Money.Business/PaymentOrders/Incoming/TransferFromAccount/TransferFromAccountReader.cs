using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount
{
    [InjectAsSingleton(typeof(ITransferFromAccountReader))]
    internal sealed class TransferFromAccountReader : ITransferFromAccountReader
    {
        private readonly TransferFromAccountApiClient apiClient;
        private readonly TransferToAccountApiClient otherApiClient;
        private readonly ILinksReader linksReader;
        private readonly ITransferFromAccountAccessor transferFromAccountAccessor;

        public TransferFromAccountReader(
            TransferFromAccountApiClient apiClient,
            TransferToAccountApiClient otherApiClient,
            ILinksReader linksReader,
            ITransferFromAccountAccessor transferFromAccountAccessor)
        {
            this.apiClient = apiClient;
            this.otherApiClient = otherApiClient;
            this.linksReader = linksReader;
            this.transferFromAccountAccessor = transferFromAccountAccessor;
        }

        public async Task<TransferFromAccountResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            response.IsReadOnly = await transferFromAccountAccessor.IsReadOnlyAsync(response);
            if (response.FromSettlementAccountId <= 0)
            {
                var links = await linksReader.GetLinksWithDocumentsAsync(documentBaseId);
                var otherDocumentBaseId = links.FirstOrDefault(x => x.Document.Type == LinkedDocumentType.PaymentOrder)?.Document.Id;
                if (otherDocumentBaseId != null)
                {
                    var otherPaymentOrder = await otherApiClient.GetAsync(otherDocumentBaseId.Value);
                    if (otherPaymentOrder != null && response.SettlementAccountId == otherPaymentOrder.ToSettlementAccountId)
                    {
                        response.FromSettlementAccountId = otherPaymentOrder.SettlementAccountId;
                    }
                }
            }
            return response;
        }
    }
}