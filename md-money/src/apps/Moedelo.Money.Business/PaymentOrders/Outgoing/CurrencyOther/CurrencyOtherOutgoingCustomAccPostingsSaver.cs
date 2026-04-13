using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.AccPostings;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.SettlementAccounts;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Enums;
using Moedelo.Requisites.Enums.SettlementAccounts;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther
{
    [InjectAsSingleton(typeof(CurrencyOtherOutgoingCustomAccPostingsSaver))]
    internal sealed class CurrencyOtherOutgoingCustomAccPostingsSaver
    {
        private readonly ICustomAccPostingsSaver customAccPostingsSaver;
        private readonly CustomAccPostingsRemover customAccPostingsRemover;
        private readonly IFirmRequisitesReader firmRequisitesReader;
        private readonly ISettlementAccountsReader settlementAccountsReader;

        public CurrencyOtherOutgoingCustomAccPostingsSaver(
            ICustomAccPostingsSaver customAccPostingsSaver,
            CustomAccPostingsRemover customAccPostingsRemover,
            IFirmRequisitesReader firmRequisitesReader,
            ISettlementAccountsReader settlementAccountsReader)
        {
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.customAccPostingsRemover = customAccPostingsRemover;
            this.firmRequisitesReader = firmRequisitesReader;
            this.settlementAccountsReader = settlementAccountsReader;
        }

        public async Task OverwriteAsync(CurrencyOtherOutgoingSaveRequest request)
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
                var creditCode = await GetCreditCodeAsync(request.SettlementAccountId);
                await customAccPostingsSaver.OverwriteAsync(
                    request.DocumentBaseId,
                    OperationType.PaymentOrderOutgoingCurrencyOther,
                    CurrencyOtherOutgoingMapper.MapToPostings(request.AccPosting, creditCode));
                return;
            }

            await customAccPostingsRemover.DeleteAsync(request.DocumentBaseId);
        }

        private async Task<int> GetCreditCodeAsync(int settlementAccountId)
        {
            var settlementAccount = await settlementAccountsReader.GetByIdAsync(settlementAccountId);
            switch (settlementAccount.Type)
            {
                case SettlementAccountType.Currency:
                    return (int)Moedelo.AccPostings.Enums.SyntheticAccountCode._52_01_02;
                case SettlementAccountType.Transit:
                    return (int)Moedelo.AccPostings.Enums.SyntheticAccountCode._52_01_01;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}