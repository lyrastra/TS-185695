using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingIncomeFromCommissionAgent)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class IncomeFromCommissionAgentDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IIncomeFromCommissionAgentReader reader;
        private readonly IIncomeFromCommissionAgentUpdater updater;

        public IncomeFromCommissionAgentDuplicateMerger(
            IIncomeFromCommissionAgentReader reader,
            IIncomeFromCommissionAgentUpdater updater)
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
            var saveRequest = IncomeFromCommissionAgentMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest);
        }
    }
}
