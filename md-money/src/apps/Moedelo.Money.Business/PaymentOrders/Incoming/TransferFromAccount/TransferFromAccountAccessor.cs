using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Business.LinkedDocuments.Links;
using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount
{
    [InjectAsSingleton(typeof(ITransferFromAccountAccessor))]
    internal sealed class TransferFromAccountAccessor : ITransferFromAccountAccessor
    {
        private readonly IPaymentOrderAccessor paymentOrderAccessor;
        private readonly ILinksReader linksReader;

        public TransferFromAccountAccessor(
            IPaymentOrderAccessor paymentOrderAccessor,
            ILinksReader linksReader)
        {
            this.paymentOrderAccessor = paymentOrderAccessor;
            this.linksReader = linksReader;
        }

        public async Task<bool> IsReadOnlyAsync(TransferFromAccountResponse payment)
        {
            var isReadOnlyCommon = paymentOrderAccessor.IsReadOnly(payment);

            if (isReadOnlyCommon)
            {
                return true;
            }

            var links = await linksReader.GetLinksWithDocumentsAsync(payment.DocumentBaseId);
            return links.Length > 0;
        }
    }
}
