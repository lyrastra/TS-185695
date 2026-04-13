using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.RetailReports.Dto
{
    public class RetailReportDto
    {
        public long Id { get; set; }

        public long BaseId { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Период отчета (начало)
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Период отчета (окончание)
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public IList<RetailReportItemDto> Items { get; set; }

        /// <summary>
        /// Id склада
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// Список идентификаторы связных платёжных документов
        /// Если поле не указано или равно null, то состав связей не меняется
        /// </summary>
        public List<RetailReportReasonRevenueDto> ReasonRevenues { get; set; } = null;
    }
}
