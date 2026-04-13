using System;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Dto
{
    public class NdsDeductionDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// Дата принятия к вычету
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма принятая к вычету
        /// </summary>
        public decimal Sum { get; set; }
    }
}