using System;
using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.Providing.TaxPostings;

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