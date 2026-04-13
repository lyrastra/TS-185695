using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Balances
{
    public class CurrencyRemainsDto
    {
        public decimal Sum { get; set; }

        public bool IsCredit { get; set; }

        public CurrencySyntheticAccountCode Code { get; set; }

        public long SubcontoId { get; set; }

        /// <summary>
        /// Сумма остатка в валюте. Используется для счетов 52.01.01 и 52.01.02
        /// </summary>
        public decimal? CurrencySum { get; set; }
    }
}