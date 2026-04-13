using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.OpeningBalance;

namespace Moedelo.AccountingV2.Dto.Remains
{
    /// <summary>
    /// Остатки по субсчёту
    /// </summary>
    public class AccountOpeningBalancesModel
    {
        /// <summary>
        /// Номер субсчёта
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// Тип: дебет/кредит
        /// </summary>
        public OpeningBalanceType Type { get; set; }

        /// <summary>
        /// Список остатков по объектам учёта (субконто)
        /// </summary>
        public IReadOnlyCollection<AccountOpeningBalancesRowModel> Balances { get; set; }
    }
}
