using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Remains
{
    public class OpeningBalancesSaveRequestModel
    {
        /// <summary>
        /// Дата ввода остатков
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Остатки по субсчетам
        /// </summary>
        public IReadOnlyCollection<AccountOpeningBalancesModel> Accounts { get; set; }

        /// <summary>
        /// Флаг отключает валидацию и пытается импортировать только то, что пройдёт валидацию
        /// </summary>
        public bool SkipInvalidRows { get; set; }
    }
}
