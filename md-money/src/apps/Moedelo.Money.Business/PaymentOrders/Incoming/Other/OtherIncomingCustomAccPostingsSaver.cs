using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.AccPostings;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using Moedelo.Money.Enums;
using System.Threading.Tasks;
using Moedelo.Money.Enums.Extensions;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other
{
    [InjectAsSingleton(typeof(OtherIncomingCustomAccPostingsSaver))]
    internal sealed class OtherIncomingCustomAccPostingsSaver
    {
        private readonly ICustomAccPostingsSaver customAccPostingsSaver;
        private readonly CustomAccPostingsRemover customAccPostingsRemover;
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public OtherIncomingCustomAccPostingsSaver(
            ICustomAccPostingsSaver customAccPostingsSaver,
            CustomAccPostingsRemover customAccPostingsRemover,
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.customAccPostingsRemover = customAccPostingsRemover;
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task OverwriteAsync(OtherIncomingSaveRequest request)
        {
            var isOoo = await firmRequisitesReader.IsOooAsync();
            if (!isOoo)
            {
                return;
            }

            // если операция в "красной таблице", следовало бы НЕ сохранять проводку
            // но тогда теряется ее структура при открытии на редактирование
            if (request.ProvideInAccounting && request.AccPosting != null)
            {
                await customAccPostingsSaver.OverwriteAsync(
                    request.DocumentBaseId,
                    OperationType.PaymentOrderIncomingOther,
                    OtherIncomingMapper.MapToPostings(request.AccPosting));
                return;
            }

            await customAccPostingsRemover.DeleteAsync(request.DocumentBaseId);
        }
    }
}