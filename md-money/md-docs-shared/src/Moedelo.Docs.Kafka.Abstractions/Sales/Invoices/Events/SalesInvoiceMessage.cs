using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Invoices.Events
{
    public class SalesInvoiceMessage
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
        /// Сумма вычета
        /// </summary>
        public decimal? NdsDeductionSum { get; set; }

        /// <summary>
        /// Номер платежного поручения
        /// </summary>
        public string PaymentNumber { get; set; }

        /// <summary>
        /// Дата платежного поручения
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Статус подписи. (Не подписан, Скан, Подписан)
        /// </summary>
        public SignStatus SignStatus { get; set; }
    }
}