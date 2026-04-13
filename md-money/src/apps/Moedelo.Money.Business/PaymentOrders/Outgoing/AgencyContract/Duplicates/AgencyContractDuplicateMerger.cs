using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.AgencyContract.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingAgencyContract)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class AgencyContractDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IAgencyContractReader reader;
        private readonly IAgencyContractUpdater updater;

        public AgencyContractDuplicateMerger(
            IAgencyContractReader reader,
            IAgencyContractUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task MergeAsync(PaymentOrderDuplicateMergeRequest request)
        {
            var response = await reader.GetByBaseIdAsync(request.DocumentBaseId);
            if (response.Date == request.Date)
            {
                return;
            }
            var saveRequest = AgencyContractMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest);
        }
    }
}
