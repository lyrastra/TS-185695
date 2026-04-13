using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount.Duplicates
{
    [OperationType(OperationType.MemorialWarrantWithdrawalFromAccount)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class WithdrawalFromAccountDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IWithdrawalFromAccountReader reader;
        private readonly IWithdrawalFromAccountUpdater updater;

        public WithdrawalFromAccountDuplicateMerger(
            IWithdrawalFromAccountReader reader,
            IWithdrawalFromAccountUpdater updater)
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
            var saveRequest = WithdrawalFromAccountMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
