using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromPurse
{
    [InjectAsSingleton(typeof(ITransferFromPurseReader))]
    internal sealed class TransferFromPurseReader : ITransferFromPurseReader
    {
        private readonly TransferFromPurseApiClient apiClient;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public TransferFromPurseReader(
            TransferFromPurseApiClient apiClient,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<TransferFromPurseResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}