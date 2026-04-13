using System;
using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Balances
{
    public class AccountBalanceDto
    {
        /// <summary>
        /// Дата ввода остатка
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма остатка в рублях
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма остатка в валюте. Используется для счетов 52.01.01 и 52.01.02
        /// </summary>
        public decimal? CurrencySum { get; set; }

        public bool IsCredit { get; set; }

        public SyntheticAccountCode Code { get; set; }

        public long? SubcontoId { get; set; }
    }
}
