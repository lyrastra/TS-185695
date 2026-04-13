using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Statements.Sales
{
    public class SalesStatementCollectionItemDto
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
        /// Контрагент
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// По счету
        /// </summary>
        public long? BillId { get; set; }

        /// <summary>
        /// Счета
        /// </summary>
        public List<long?> BillIds { get; set; }

        /// <summary>
        /// По договору
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// По договору в т.ч. основному
        /// </summary>
        public long? ContractId { get; set; }

        /// <summary>
        /// Тип начисления НДС 0 - Нет, 1 - Сверху, 2 - В том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Признак документа "Подписан" (Да, Нет, Скан)
        /// </summary>
        public string OnHands { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }
    }
}