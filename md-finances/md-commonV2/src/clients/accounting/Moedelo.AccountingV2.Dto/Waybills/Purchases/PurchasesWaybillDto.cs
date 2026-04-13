using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Waybills.Purchases
{
    public class PurchasesWaybillDto
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
        /// Тип
        /// </summary>
        public WayBillTypesEnum Type { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Договор с контрагентом
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Тип начисления НДС 0 - Нет, 1 - Сверху, 2 - В том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Id склада, если товар/материал оприходован на склад
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Компенсируется ли документ заказчиком
        /// </summary>
        public bool IsCompensated { get; set; }

        /// <summary>
        /// Заказчик
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Посреднический договор
        /// </summary>
        public long? CustomerProjectId { get; set; }

        /// <summary>
        ///  Позиции накладной
        /// </summary>
        public IList<PurchasesWaybillItemDto> Items { get; set; }

        /// <summary>
        /// Счет-фактура
        /// </summary>
        public PurchasesLinkedInvoiceDto Invoice { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public DocumentContextDto Context { get; set; }

        /// <summary>
        /// Провести в бух. учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }

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