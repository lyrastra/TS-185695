using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencySale)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class IncomingCurrencySaleDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IIncomingCurrencySaleReader reader;
        private readonly IIncomingCurrencySaleUpdater updater;

        public IncomingCurrencySaleDuplicateMerger(
            IIncomingCurrencySaleReader reader,
            IIncomingCurrencySaleUpdater updater)
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
            var saveRequest = IncomingCurrencySaleMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
