using System;
using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.BackofficeBilling.Bills.BillRequest
{
    public class ProductConfigurationRequestDto
    {
        /// <summary>
        /// Срок действия продуктовой услуги
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Технический код продуктовой услуги
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Набор модификаторов продуктовой услуги
        /// </summary>
        public IReadOnlyDictionary<string, ModifierRequestDto> ModifiersValues { get; set; }

        /// <summary>
        /// Опционально. Дата начала срока действия
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Опционально. Дата окончания срока действия (требуется для допродаж)
        /// </summary>
        public DateTime? EndDate { get; set; }

    }
}
