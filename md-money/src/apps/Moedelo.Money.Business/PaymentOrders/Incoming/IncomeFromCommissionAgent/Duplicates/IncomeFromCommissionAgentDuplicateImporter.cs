using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingIncomeFromCommissionAgent)]
    [InjectAsSingleton(typeof(IConcreteDuplicateImporter))]
    internal sealed class IncomeFromCommissionAgentDuplicateImporter : IConcreteDuplicateImporter
    {
        private readonly IIncomeFromCommissionAgentReader reader;
        private readonly IIncomeFromCommissionAgentUpdater updater;

        public IncomeFromCommissionAgentDuplicateImporter(
            IIncomeFromCommissionAgentReader reader,
            IIncomeFromCommissionAgentUpdater updater)
        {
            this.reader = reader;
            this.updater = updater;
        }

        public async Task ImportAsync(long documentBaseId)
        {
            var response = await reader.GetByBaseIdAsync(documentBaseId);
            var saveRequest = IncomeFromCommissionAgentMapper.MapToSaveRequest(response);
            saveRequest.DuplicateId = null;
            saveRequest.OperationState = OperationState.Default;
            await updater.UpdateAsync(saveRequest);
        }
    }
}
