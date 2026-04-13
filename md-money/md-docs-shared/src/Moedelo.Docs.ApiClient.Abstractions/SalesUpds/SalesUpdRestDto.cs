using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesUpds
{
    /// <summary>
    /// Модель УПД продажи, используемая в external API
    /// </summary>
    public class SalesUpdRestDto
    {
        /// <summary>
        /// Числовой идентификатор
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
        /// Статус УПД
        /// </summary>
        public UpdStatus Status { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Грузоотправитель 
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// Грузополучатель
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Идентификатор договора (по договору)
        /// </summary>
        public long? ContractId { get; set; }
        
        /// <summary>
        /// Идентификатор счета (по счету)
        /// </summary>
        public long? BillId { get; set; }

        /// <summary>
        /// Тип начисления НДС (сверху/в т.ч.)
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// В какой системе налогообложения учитывается документ (в случае смешанной СНО)
        /// </summary>
        public TaxationSystemType TaxSystem { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<SalesUpdItemDto> Items { get; set; }
        
        /// <summary>
        /// Склад, с которого списаны товары
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Связанные платежи
        /// </summary>
        public List<SalesUpdPaymentDto> Payments { get; set; }

        /// <summary>
        /// Вычеты НДС по связанным авансовым счетам-фактурам
        /// </summary>
        public List<SalesNdsDeductionByAdvanceInvoiceDto> NdsDeductions { get; set; }
    }
}