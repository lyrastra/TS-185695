using System;

namespace Moedelo.Docs.Dto.NdsAdjustment
{
    public class NdsAccrualItemDto
    {
        /// <summary>
        /// Идентификатор платёжного документа
        /// </summary>
        public long PaymentDocumentBaseId { get; set; }

        /// <summary>
        /// Дата платёжного документа
        /// </summary>
        public DateTime PaymentDocumentDate { get; set; }

        /// <summary>
        /// Номер платёжного документа
        /// </summary>
        public string PaymentDocumentNumber { get; set; }

        /// <summary>
        /// Тип платёжного документа
        /// </summary>
        public OperationSource PaymentOperationSource { get; set; }

        /// <summary>
        /// Номер авансовой с/ф
        /// </summary>
        public string AdvanceInvoiceNumber { get; set; }

        /// <summary>
        /// Дата авансовой с/ф
        /// </summary>
        public DateTime AdvanceInvoiceDate { get; set; }

        /// <summary>
        /// Сумма НДС документа
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Тип НДС
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Сумма принятого НДС к вычету
        /// </summary>
        public decimal? NdsDeductionAccepted { get; set; }

        /// <summary>
        /// Документ в закрытом периоде
        /// </summary>
        public bool InClosedPeriod { get; set; }

        /// <summary>
        /// Наименование контрагента
        /// </summary>
        public string KontragentName { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }
        
        /// <summary>
        /// Платёж игнорируется при создании авансовых с/ф
        /// </summary>
        public bool IsSkipped { get; set; }
    }
}