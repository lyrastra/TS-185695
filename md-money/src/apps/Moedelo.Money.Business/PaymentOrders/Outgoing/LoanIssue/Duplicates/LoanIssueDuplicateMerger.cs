using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanIssue.Duplicates
{
    [OperationType(OperationType.PaymentOrderOutgoingLoanIssue)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class LoanIssueDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly ILoanIssueReader reader;
        private readonly ILoanIssueUpdater updater;

        public LoanIssueDuplicateMerger(
            ILoanIssueReader reader,
            ILoanIssueUpdater updater)
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
            var saveRequest = LoanIssueMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
