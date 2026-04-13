using System;
using System.Collections.Generic;
using Moedelo.AccountingV2.Dto.PrimaryDocuments;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Invoices.Sales
{
    public class SalesInvoiceDto
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
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Тип начисления НДС 1 - не начислять, 2 - сверху, 3 - в том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Грузоотправитель
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// Поставщик
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// Грузополучатель
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Плательщик
        /// </summary>
        public int? PayerId { get; set; }

        /// <summary>
        /// Счет
        /// </summary>
        public long? BillId { get; set; }

        /// <summary>
        /// Конрагент
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Признак документа "Подписан" (Да, Нет, Скан)
        /// </summary>
        public string OnHands { get; set; }
        
        /// <summary>
        /// Признак "Основной вид деятельности"
        /// </summary>
        public bool IsMainActivity { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<SalesInvoiceItemDto> Items { get; set; }

        /// <summary>
        /// Номер платежного поручения
        /// </summary>
        public string PaymentNumber { get; set; }

        /// <summary>
        /// Дата платежного поручения
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Информация об изменениях документа
        /// </summary>
        public DocumentContextDto Context { get; set; }

        /// <summary>
        /// Первичный документ (документ основание)
        /// </summary>
        public SalesInvoiceLinkedDocumentDto ReasonDocument { get; set; }

        /// <summary>
        /// Идентификатор госконтракта (ИГК)
        /// </summary>
        public string GovernmentContractId { get; set; }
    }
}