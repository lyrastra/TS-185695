using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionToAuthorizedCapital.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingContributionToAuthorizedCapital)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class ContributionToAuthorizedCapitalDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IContributionToAuthorizedCapitalReader reader;
        private readonly IContributionToAuthorizedCapitalUpdater updater;

        public ContributionToAuthorizedCapitalDuplicateMerger(
            IContributionToAuthorizedCapitalReader reader,
            IContributionToAuthorizedCapitalUpdater updater)
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
            var saveRequest = ContributionToAuthorizedCapitalMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
