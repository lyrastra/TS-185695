using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.AgencyContract.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingAgencyContract)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class AgencyContractDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IAgencyContractReader reader;
        private readonly IAgencyContractUpdater updater;

        public AgencyContractDuplicateImporter(
            IAgencyContractReader reader,
            IAgencyContractUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            var saveRequest = AgencyContractMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest);
        }
    }
}
