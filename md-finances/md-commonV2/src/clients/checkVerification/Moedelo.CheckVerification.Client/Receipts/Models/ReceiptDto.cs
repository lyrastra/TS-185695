using System;

namespace Moedelo.CheckVerification.Client.Receipts.Models
{
    public class ReceiptDto
    {
        /// <summary>
        /// Дата и время чека
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Номер смены
        /// </summary>
        public int ShiftNumber { get; set; }

        /// <summary>
        /// Номер чека
        /// </summary>
        public int RequestNumber { get; set; }

        /// <summary>
        /// Общая сумма чека
        /// </summary>
        public decimal TotalSum { get; set; }

        /// <summary>
        /// Общая сумма наличных
        /// </summary>
        public int CashTotalSum { get; set; }

        /// <summary>
        /// Общая сумма электронных платежей
        /// </summary>
        public decimal EcashTotalSum { get; set; }

        public int ReceiptCode { get; set; }
        
        /// <summary>
        /// Кассир
        /// </summary>
        public string Operator { get; set; }

        public int OperationType { get; set; }

        /// <summary>
        /// Номер фискального накопителя (ФН)
        /// </summary>
        public string FiscalDriveNumber { get; set; }
        
        /// <summary>
        /// Номер фискального документа (ФД)
        /// </summary>
        public int FiscalDocumentNumber { get; set; }

        /// <summary>
        /// Фискальный признак (ФП)
        /// </summary>
        public long FiscalSign { get; set; }
        
        /// <summary>
        /// ИНН Контрагента
        /// </summary>
        public string UserInn { get; set; }
        
        /// <summary>
        /// Рег. номер ККТ
        /// </summary>
        public string KktRegId { get; set; }
        
        /// <summary>
        /// Сумма НДС по ставке 20%
        /// </summary>
        public decimal TotalNds20Sum { get; set; }
        
        /// <summary>
        /// Сумма НДС по ставке 10%
        /// </summary>
        public decimal TotalNds10Sum { get; set; }
        
        /// <summary>
        /// СНО Контрагента: 1 - ОСНО, 2 - УСН "Доходы", 4 - УСН "Доходы - расходы", 8 - ЕНВД
        /// </summary>
        public int TaxationType { get; set; }

        /// <summary>
        /// Список позиций чека
        /// </summary>
        public ReceiptItemDto[] Items { get; set; }
    }
}