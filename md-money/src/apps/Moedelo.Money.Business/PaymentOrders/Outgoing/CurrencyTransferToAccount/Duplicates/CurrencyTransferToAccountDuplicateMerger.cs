using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyTransferToAccount)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class CurrencyTransferToAccountDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ICurrencyTransferToAccountReader reader;
        private readonly ICurrencyTransferToAccountUpdater updater;

        public CurrencyTransferToAccountDuplicateMerger(
            ICurrencyTransferToAccountReader reader,
            ICurrencyTransferToAccountUpdater updater)
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
            var saveRequest = CurrencyTransferToAccountMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            saveRequest.ProvideInAccounting = true;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
