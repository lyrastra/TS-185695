using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.AccPostings;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.SettlementAccounts;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Enums;
using Moedelo.Requisites.Enums.SettlementAccounts;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyOther
{
    [InjectAsSingleton(typeof(CurrencyOtherIncomingCustomAccPostingsSaver))]
    internal sealed class CurrencyOtherIncomingCustomAccPostingsSaver
    {
        private readonly ICustomAccPostingsSaver customAccPostingsSaver;
        private readonly CustomAccPostingsRemover customAccPostingsRemover;
        private readonly IFirmRequisitesReader firmRequisitesReader;
        private readonly ISettlementAccountsReader settlementAccountsReader;

        public CurrencyOtherIncomingCustomAccPostingsSaver(
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

        public async Task OverwriteAsync(CurrencyOtherIncomingSaveRequest request)
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
                var debitCode = await GetDebitCodeAsync(request.SettlementAccountId);
                await customAccPostingsSaver.OverwriteAsync(
                    request.DocumentBaseId,
                    OperationType.PaymentOrderIncomingCurrencyOther,
                    CurrencyOtherIncomingMapper.MapToPostings(request.AccPosting, debitCode));
                return;
            }

            await customAccPostingsRemover.DeleteAsync(request.DocumentBaseId);
        }

        private async Task<int> GetDebitCodeAsync(int settlementAccountId)
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