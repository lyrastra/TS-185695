using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Business.PaymentOrders.Duplicates;
using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BankFee.Duplicates
{
    [OperationType(OperationType.MemorialWarrantBankFee)]
    [InjectAsSingleton(typeof(IConcreteDuplicateMerger))]
    internal sealed class BankFeeDuplicateMerger : IConcreteDuplicateMerger
    {
        private readonly IBankFeeReader reader;
        private readonly IBankFeeUpdater updater;

        public BankFeeDuplicateMerger(
            IBankFeeReader reader,
            IBankFeeUpdater updater)
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
            var saveRequest = BankFeeMapper.MapToSaveRequest(response);
            saveRequest.Date = request.Date;
            await updater.UpdateAsync(saveRequest).ConfigureAwait(false);
        }
    }
}
