using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.WithdrawalOfProfit.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingWithdrawalOfProfit)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class WithdrawalOfProfitDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IWithdrawalOfProfitReader reader;
        private readonly IWithdrawalOfProfitUpdater updater;

        public WithdrawalOfProfitDuplicateImporter(
            IWithdrawalOfProfitReader reader,
            IWithdrawalOfProfitUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = WithdrawalOfProfitMapper.MapToSaveRequest(response);
            saveRequest.IsPaid = true;
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
