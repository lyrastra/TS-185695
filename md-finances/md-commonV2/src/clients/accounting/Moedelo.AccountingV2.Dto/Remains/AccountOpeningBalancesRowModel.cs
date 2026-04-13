using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Remains
{
    public class AccountOpeningBalancesRowModel
    {
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        ///  Сумма в валюте (для счетов 52.01.01 и 52.01.02)
        /// </summary>
        public decimal? CurrencySum { get; set; }

        /// <summary>
        /// Количество (для счетов 41.01 и 41.02)
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// Список субконто (см. схему по /api/v1/syntheticaccount для заданного счёта)
        /// </summary>
        public IReadOnlyCollection<long> Subconto { get; set; }

        /// <summary>
        /// Документы
        /// </summary>
        public IReadOnlyCollection<OpeningBalancesDocumentModel> Documents { get; set; }
    }
}
