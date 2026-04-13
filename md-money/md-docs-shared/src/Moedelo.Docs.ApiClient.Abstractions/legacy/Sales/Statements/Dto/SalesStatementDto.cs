using System;
using System.Collections.Generic;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Statements.Dto
{
    public class SalesStatementDto
    {
        /// <summary>
        /// Id документа (Сквозная нумерация по всем типам документов)
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер документа (уникальный в пределах года)
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocDate { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Код контрагента
        /// </summary>
        public int KontragentAccountCode { get; set; }

        /// <summary>
        /// По счету
        /// </summary>
        public long? BillId { get; set; }

        /// <summary>
        /// По договору
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Тип начисления НДС 0 - Нет, 1 - Сверху, 2 - В том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Признак документа "Подписан" (Да, Нет, Скан)
        /// </summary>
        public string OnHands { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Связанный счет-фактура
        /// </summary>
        public LinkedInvoiceDto Invoice { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<SalesStatementItemDto> Items { get; set; }

        /// <summary>
        /// Информация об изменениях документа
        /// </summary>
        public DocumentContextDto Context { get; set; }
    }
}
