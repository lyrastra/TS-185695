using System;
using System.Collections.Generic;

namespace Moedelo.Money.Dto.PaymentOrders.Outgoing.BudgetaryPayment.UnifiedBudgetaryPayment
{
    /// <summary>
    /// Параметры налогового учёта
    /// </summary>
    public class TaxPostingsSaveDto
    {
        /// <summary>
        /// Режим создания проводок
        /// Автоматически = false
        /// В ручном режиме = true
        /// </summary>
        public bool IsManual { get; set; }

        /// <summary>
        /// Список проводок (для УСН, ИП ОСНО, ООО ОСНО)
        /// </summary>
        public IReadOnlyList<TaxPostingCommonDto> Postings { get; set; } = Array.Empty<TaxPostingCommonDto>();
    }
}