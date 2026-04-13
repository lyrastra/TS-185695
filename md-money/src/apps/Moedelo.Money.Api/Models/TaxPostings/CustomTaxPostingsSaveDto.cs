using System;
using System.Collections.Generic;

namespace Moedelo.Money.Api.Models.TaxPostings
{
    /// <summary>
    /// Параметры налогового учёта (вручную)
    /// </summary>
    public class CustomTaxPostingsSaveDto
    {
        /// <summary>
        /// Список проводок (для УСН, ИП ОСНО, ООО ОСНО)
        /// </summary>
        public IReadOnlyList<TaxPostingCommonDto> Postings { get; set; } = Array.Empty<TaxPostingCommonDto>();
    }
}
