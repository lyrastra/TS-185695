using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moedelo.Money.Api.Models.TaxPostings
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
        [DefaultValue(false)]
        public bool IsManual { get; set; }

        /// <summary>
        /// Список проводок (для УСН, ИП ОСНО, ООО ОСНО)
        /// </summary>
        public IReadOnlyList<TaxPostingCommonDto> Postings { get; set; } = Array.Empty<TaxPostingCommonDto>();
    }
}
