using System;
using System.Collections.Generic;
using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Invoices.Purchases
{
    public class PurchasesCommonInvoiceSaveRequestDto
    {
        /// <summary>
        /// Id документа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime? DocDate { get; set; }

        /// <summary>
        /// Id контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Тип начисления НДС 0 - Нет, 1 - Сверху, 2 - В том числе
        /// </summary>
        public NdsPositionType? NdsPositionType { get; set; }

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
        public List<PurchasesCommonInvoiceItemSaveRequestDto> Items { get; set; }

        /// <summary>
        /// Авансовые сч-фактуры (для НДС к восстановлению)
        /// </summary>
        public List<AdvanceInvoiceDto> AdvanceInvoices { get; set; }
    }
}