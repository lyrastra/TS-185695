using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount
{
    [InjectAsSingleton(typeof(RefundToSettlementAccountProvideInAccountingFixer))]
    internal sealed class RefundToSettlementAccountProvideInAccountingFixer
    {
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public RefundToSettlementAccountProvideInAccountingFixer(
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task FixAsync(RefundToSettlementAccountSaveRequest request)
        {
            if (!request.ProvideInAccounting)
            {
                var isOoo = await firmRequisitesReader.IsOooAsync();
                request.ProvideInAccounting = !isOoo;
            }
        }
    }
}
