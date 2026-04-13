using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.AccPostings;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    [InjectAsSingleton(typeof(DeductionCustomAccPostingsSaver))]
    internal sealed class DeductionCustomAccPostingsSaver
    {
        private readonly ICustomAccPostingsSaver customAccPostingsSaver;
        private readonly CustomAccPostingsRemover customAccPostingsRemover;
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public DeductionCustomAccPostingsSaver(
            ICustomAccPostingsSaver customAccPostingsSaver,
            CustomAccPostingsRemover customAccPostingsRemover,
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.customAccPostingsRemover = customAccPostingsRemover;
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task OverwriteAsync(DeductionSaveRequest request)
        {
            var isOoo = await firmRequisitesReader.IsOooAsync();
            if (!isOoo)
            {
                return;
            }

            if (request.IsPaid && request.ProvideInAccounting && request.AccountingPosting != null)
            {
                await customAccPostingsSaver.OverwriteAsync(
                    request.DocumentBaseId,
                    OperationType.PaymentOrderOutgoingOther,
                    DeductionMapper.MapToPostings(request.AccountingPosting));
                return;
            }

            await customAccPostingsRemover.DeleteAsync(request.DocumentBaseId);
        }
    }
}