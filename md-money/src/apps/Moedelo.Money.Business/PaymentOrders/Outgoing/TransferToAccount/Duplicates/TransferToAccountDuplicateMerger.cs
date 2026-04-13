using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingTransferToAccount)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class TransferToAccountDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ITransferToAccountReader reader;
        private readonly ITransferToAccountUpdater updater;

        public TransferToAccountDuplicateMerger(
            ITransferToAccountReader reader,
            ITransferToAccountUpdater updater)
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
            var saveRequest = TransferToAccountMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
