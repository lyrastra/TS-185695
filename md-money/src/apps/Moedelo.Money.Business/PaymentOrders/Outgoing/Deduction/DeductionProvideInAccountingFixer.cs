using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    [InjectAsSingleton(typeof(DeductionProvideInAccountingFixer))]
    internal sealed class DeductionProvideInAccountingFixer
    {
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public DeductionProvideInAccountingFixer(
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task FixAsync(DeductionSaveRequest request)
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
