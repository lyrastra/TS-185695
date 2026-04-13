using Moedelo.Common.Enums.Enums.SyntheticAccounts;

namespace Moedelo.AccountingV2.Dto.Balances
{
    public class BalanceDto
    {
        public decimal Sum { get; set; }

        public bool IsCredit { get; set; }

        public SyntheticAccountCode Code { get; set; }

        public long? SubcontoId { get; set; }

        /// <summary>
        /// Сумма остатка в валюте. Используется для счетов 52.01.01 и 52.01.02
        /// </summary>
        public decimal? CurrencySum { get; set; }
    }
}