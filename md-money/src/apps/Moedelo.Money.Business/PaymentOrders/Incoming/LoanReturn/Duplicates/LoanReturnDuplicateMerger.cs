using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanReturn.Duplicates
{
    [OperationType(OperationType.PaymentOrderIncomingLoanReturn)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class LoanReturnDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ILoanReturnReader reader;
        private readonly ILoanReturnUpdater updater;

        public LoanReturnDuplicateMerger(
            ILoanReturnReader reader,
            ILoanReturnUpdater updater)
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
            var saveRequest = LoanReturnMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
