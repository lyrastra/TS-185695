using System;
using System.Collections.Generic;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Waybills.Dto
{
    /// <summary>
    /// Данные по накладной
    /// </summary>
    public class SalesWaybillDto
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
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Тип начисления НДС 1 - не начислять, 2 - сверху, 3 - в том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Id грузоотправителя
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// Id поставщика
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// Id грузополучателя
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Id плательщика
        /// </summary>
        public int? PayerId { get; set; }

        /// <summary>
        /// Id контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Id связанного счета
        /// </summary>
        public long? BillId { get; set; }

        /// <summary>
        /// Id связанного договора
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Id склада с которого происходит продажа
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Провести в бух. учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Признак документа "Подписан" (Да, Нет, Скан)
        /// </summary>
        public string OnHands { get; set; }

        /// <summary>
        /// Счет-фактура
        /// </summary>
        public LinkedInvoiceDto Invoice { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<SalesWaybillItemDto> Items { get; set; }

        /// <summary>
        /// Информация об изменениях документа
        /// </summary>
        public DocumentContextDto Context { get; set; }
    }
}
