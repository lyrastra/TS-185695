using System;
using System.Collections.Generic;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Waybills.Dto
{
    public class PurchasesWaybillSaveRequestDto
    {
        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime? DocDate { get; set; }
        
        // <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Договор с контрагентом
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Тип начисления НДС 0 - Нет, 1 - Сверху, 2 - В том числе
        /// </summary>
        public NdsPositionType? NdsPositionType { get; set; }

        /// <summary>
        /// Счет-фактура
        /// </summary>
        public LinkedInvoiceDto Invoice { get; set; }

        /// <summary>
        /// Компенсируется ли документ заказчиком
        /// </summary>
        public bool IsCompensated { get; set; }

        /// <summary>
        /// Заказчик
        /// Если документ компенсируется заказчиком, нужно указать заказчика
        /// Не может совпадать со значением KontragentId (контрагент не может выступать в качестве заказчика)
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Посреднический договор
        /// Если документ компенсируется заказчиком, нужно указать посреднический договор
        /// </summary>
        public long? CustomerProjectId { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<PurchasesWaybillItemSaveRequestDto> Items { get; set; }

        /// <summary>
        /// Id склада, если товар/материал оприходован на склад
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Система налогообложения, в которой будет учтён документ (1 - УСНО, 2 - ОСНО, 3 - ЕНВД)
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }
        
        /// <summary>
        /// Наличие несоответствия по количеству/качеству
        /// </summary>
        public bool DiscrepancyNumberOrQuality { get; set; }
    }
}