using System;
using System.Collections.Generic;
using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Invoices.Purchases
{
    public class PurchasesCommonInvoiceDto
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
        /// Поставщик
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Тип начисления НДС 0 - Нет, 1 - Сверху, 2 - В том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }
        
        /// <summary>
        /// Документ-основание
        /// </summary>
        public long? DocReasonId { get; set; }

        /// <summary>
        /// НДС к вычету
        /// </summary>
        public List<NdsDeductionDto> NdsDeductions { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<PurchasesCommonInvoiceItemDto> Items { get; set; }

        /// <summary>
        /// Авансовые сч-фактуры (для НДС к восстановлению)
        /// </summary>
        public List<AdvanceInvoiceDto> AdvanceInvoices { get; set; }

        /// <summary>
        /// Системная информация
        /// </summary>
        public DocumentContextDto Context { get; set; }
    }
}