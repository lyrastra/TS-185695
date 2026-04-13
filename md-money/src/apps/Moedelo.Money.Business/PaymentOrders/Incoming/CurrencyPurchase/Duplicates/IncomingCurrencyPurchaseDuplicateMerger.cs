using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyPurchase)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class IncomingCurrencyPurchaseDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IIncomingCurrencyPurchaseReader reader;
        private readonly IIncomingCurrencyPurchaseUpdater updater;

        public IncomingCurrencyPurchaseDuplicateMerger(
            IIncomingCurrencyPurchaseReader reader,
            IIncomingCurrencyPurchaseUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task MergeAsync(PaymentOrderDuplicateMergeRequest request)
        {
            var response = await reader.GetByBaseIdAsync(request.DocumentBaseId).ConfigureAwait(false);
            if (response.Date == request.Date)
            {
                return;
            }
            var saveRequest = IncomingCurrencyPurchaseMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
