using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionToAuthorizedCapital.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingContributionToAuthorizedCapital)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class ContributionToAuthorizedCapitalDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IContributionToAuthorizedCapitalReader reader;
        private readonly IContributionToAuthorizedCapitalUpdater updater;

        public ContributionToAuthorizedCapitalDuplicateImporter(
            IContributionToAuthorizedCapitalReader reader,
            IContributionToAuthorizedCapitalUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            var saveRequest = ContributionToAuthorizedCapitalMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
