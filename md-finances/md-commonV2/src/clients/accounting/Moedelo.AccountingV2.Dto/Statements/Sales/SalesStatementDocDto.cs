using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Statements.Sales
{
    /// <summary>
    /// Акт из Продаж
    /// </summary>
    public class SalesStatementDocDto
    {
        /// <summary>
        /// BaseId документа (Сквозная нумерация по всем типам документов)
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа (уникальный в пределах года)
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Контрагент
        /// </summary>
        public int KontragentId { get; set; }
        
        /// <summary>
        /// Счет контрагента
        /// </summary>
        public int KontragentAccountCode { get; set; }

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

        /// <summary>
        /// Учесть в (при сдвоенной СНО)
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}