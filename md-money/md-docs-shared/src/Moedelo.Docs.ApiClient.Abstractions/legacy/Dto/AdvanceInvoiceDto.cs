using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Dto
{
    /// <summary>
    /// Авансовая счет-фактура
    /// </summary>
    public class AdvanceInvoiceDto
    {
        /// <summary>
        /// Числовой идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер авансовой счет-фактуры
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Total { get; set; }
    }
}
