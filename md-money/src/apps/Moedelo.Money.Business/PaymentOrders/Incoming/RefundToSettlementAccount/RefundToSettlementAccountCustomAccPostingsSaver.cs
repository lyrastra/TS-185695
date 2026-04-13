using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.AccPostings;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount
{
    [InjectAsSingleton(typeof(RefundToSettlementAccountCustomAccPostingsSaver))]
    internal sealed class RefundToSettlementAccountCustomAccPostingsSaver
    {
        private readonly ICustomAccPostingsSaver customAccPostingsSaver;
        private readonly CustomAccPostingsRemover customAccPostingsRemover;
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public RefundToSettlementAccountCustomAccPostingsSaver(
            ICustomAccPostingsSaver customAccPostingsSaver,
            CustomAccPostingsRemover customAccPostingsRemover,
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.customAccPostingsRemover = customAccPostingsRemover;
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task OverwriteAsync(RefundToSettlementAccountSaveRequest request)
        {
            var isOoo = await firmRequisitesReader.IsOooAsync();
            if (!isOoo)
            {
                return;
            }

            if (request.ProvideInAccounting)
            {
                await customAccPostingsSaver.OverwriteAsync(
                    request.DocumentBaseId,
                    OperationType.PaymentOrderIncomingRefundToSettlementAccount,
                    RefundToSettlementAccountMapper.MapToPostings(request.AccPosting));
                return;
            }

            await customAccPostingsRemover.DeleteAsync(request.DocumentBaseId);
        }
    }
}