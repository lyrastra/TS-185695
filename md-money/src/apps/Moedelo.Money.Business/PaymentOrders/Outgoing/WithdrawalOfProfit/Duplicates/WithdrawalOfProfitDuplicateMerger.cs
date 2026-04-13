using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingWithdrawalOfProfit)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class WithdrawalOfProfitDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IWithdrawalOfProfitReader reader;
        private readonly IWithdrawalOfProfitUpdater updater;

        public WithdrawalOfProfitDuplicateMerger(
            IWithdrawalOfProfitReader reader,
            IWithdrawalOfProfitUpdater updater)
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
            var saveRequest = WithdrawalOfProfitMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
