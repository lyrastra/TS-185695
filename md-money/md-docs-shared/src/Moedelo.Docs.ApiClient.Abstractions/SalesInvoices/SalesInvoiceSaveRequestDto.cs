using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesInvoices
{
    public class SalesInvoiceSaveRequestDto
    {
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public long? DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Идентификатор покупателя
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Идентификатор документа-основания
        /// </summary>
        public long? DocumentReasonId { get; set; }

        /// <summary>
        /// Тип документа
        /// </summary>
        public InvoiceType InvoiceType { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal SumNds { get; set; }

        /// <summary>
        /// Тип НДС
        /// </summary>
        public NdsTypes? NdsType { get; set; }

        /// <summary>
        /// Тип позиции НДС
        /// </summary>
        public NdsPositionType? NdsPositionType { get; set; }

        /// <summary>
        /// Номер платежного поручения
        /// </summary>
        public string PaymentNumber { get; set; }

        /// <summary>
        /// Дата платежного поручения
        /// </summary>
        public DateTime? PaymentDate { get; set; }
        
        /// <summary>
        /// Название авансовой с/ф
        /// </summary>
        public string AdvanceName { get; set; }

        /// <summary>
        /// Принято к вычету
        /// </summary>
        public decimal? DeductionAccepted { get; set; }
    }
}