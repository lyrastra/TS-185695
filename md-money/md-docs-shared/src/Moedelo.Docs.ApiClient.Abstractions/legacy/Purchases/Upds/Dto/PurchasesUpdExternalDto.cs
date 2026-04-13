using System;
using System.Collections.Generic;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds.Dto
{
    public class PurchasesUpdExternalDto
    {
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Идентификатор договора
        /// </summary>
        public long? ContractId { get; set; }

        /// <summary>
        /// Идентификатор склада
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// В какой системе налогообложения учитывать 1 - УСН, 2 - ОСНО, 3 - ЕНВД
        /// </summary>
        public TaxationSystemType TaxSystem { get; set; }

        /// <summary>
        /// Тип УПД 1 - Тип 1, 2 - Тип 2
        /// </summary>
        public UpdStatus Status { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<PurchasesUpdItemExternalDto> Items { get; set; }

        /// <summary>
        /// Связанные платежи
        /// </summary>
        public List<PaymentDto> Payments { get; set; }

        /// <summary>
        /// Связанная счет-фактура
        /// </summary>
        public LinkedInvoiceDto LinkedInvoice { get; set; }

        /// <summary>
        /// НДС к вычету
        /// </summary>
        public List<NdsDeductionDto> NdsDeductions { get; set; }

        /// <summary>
        /// Авансовые счета-фактуры
        /// </summary>
        public List<AdvanceInvoiceDto> AdvanceInvoices { get; set; }
    }
}