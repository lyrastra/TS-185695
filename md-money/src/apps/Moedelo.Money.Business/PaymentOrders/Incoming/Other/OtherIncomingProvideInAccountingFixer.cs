using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other
{
    [InjectAsSingleton(typeof(OtherIncomingProvideInAccountingFixer))]
    internal sealed class OtherIncomingProvideInAccountingFixer
    {
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public OtherIncomingProvideInAccountingFixer(
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task FixAsync(OtherIncomingSaveRequest request)
        {
            if (!request.ProvideInAccounting)
            {
                var isOoo = await firmRequisitesReader.IsOooAsync();
                request.ProvideInAccounting = !isOoo;
            }
        }
    }
}
