using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionOfOwnFunds.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingContributionOfOwnFunds)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class ContributionOfOwnFundsDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IContributionOfOwnFundsReader reader;
        private readonly IContributionOfOwnFundsUpdater updater;

        public ContributionOfOwnFundsDuplicateImporter(
            IContributionOfOwnFundsReader reader,
            IContributionOfOwnFundsUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = ContributionOfOwnFundsMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
