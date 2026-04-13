using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    [InjectAsSingleton(typeof(OtherOutgoingProvideInAccountingFixer))]
    internal sealed class OtherOutgoingProvideInAccountingFixer
    {
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public OtherOutgoingProvideInAccountingFixer(
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task FixAsync(OtherOutgoingSaveRequest request)
        {
            if (request.IsPaid == false)
            {
                request.ProvideInAccounting = false;
                return;
            }

            if (!request.ProvideInAccounting)
            {
                var isOoo = await firmRequisitesReader.IsOooAsync();
                request.ProvideInAccounting = !isOoo;
            }
        }
    }
}
