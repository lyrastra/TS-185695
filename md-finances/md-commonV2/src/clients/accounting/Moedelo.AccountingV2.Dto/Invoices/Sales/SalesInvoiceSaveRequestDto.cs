using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Invoices.Sales
{
    public class SalesInvoiceSaveRequestDto
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
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Тип начисления НДС 1 - не начислять, 2 - сверху, 3 - в том числе
        /// </summary>
        public NdsPositionType? NdsPositionType { get; set; }

        /// <summary>
        /// Грузоотправитель
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// Поставщик
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Плательщик
        /// </summary>
        public int? PayerId { get; set; }

        /// <summary>
        /// Id связанного счета
        /// </summary>
        public long? BillId { get; set; }

        /// <summary>
        /// Номер платежного поручения
        /// </summary>
        public string PaymentNumber { get; set; }

        /// <summary>
        /// Дата платежного поручения
        /// </summary>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<SalesInvoiceItemSaveRequestDto> Items { get; set; }

        /// <summary>
        /// Признак документа "Подписан" (Да, Нет, Скан)
        /// </summary>
        public string OnHands { get; set; }

        /// <summary>
        /// Идентификатор госконтракта (ИГК)
        /// </summary>
        public string GovernmentContractId { get; set; }
    }
}