using Moedelo.Requisites.Enums.SettlementAccounts;

namespace Moedelo.Money.Business.SettlementAccounts
{
    internal sealed class SettlementAccount
    {
        public int Id { get; set; }

        public Currency Currency { get; set; }

        public SettlementAccountType Type { get; set; }

        public int BankId { get; set; }

        public long? SubcontoId { get; set; }
    }
}