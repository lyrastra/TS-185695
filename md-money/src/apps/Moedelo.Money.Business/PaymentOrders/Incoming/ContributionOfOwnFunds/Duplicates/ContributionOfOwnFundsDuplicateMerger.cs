using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionOfOwnFunds.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingContributionOfOwnFunds)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class ContributionOfOwnFundsDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IContributionOfOwnFundsReader reader;
        private readonly IContributionOfOwnFundsUpdater updater;

        public ContributionOfOwnFundsDuplicateMerger(
            IContributionOfOwnFundsReader reader,
            IContributionOfOwnFundsUpdater updater)
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
            var saveRequest = ContributionOfOwnFundsMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
