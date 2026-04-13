using System;
using Moedelo.Accounting.Enums.Documents;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Bills
{
    /// <summary>
    /// Модель счета в массовых запросах (без позиций, платежей и некоторых других полей)
    /// </summary>
    public class SalesBillCollectionItemDto
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
        /// Тип счета 1 - Обычный, 2 - Счет-договор
        /// </summary>
        public BillType Type { get; set; }

        /// <summary>
        /// Статус счета 4 - Неоплачен, 5 - Частично оплачен, 6 - Оплачен
        /// </summary>
        public BillStatus Status { get; set; }

        /// <summary>
        /// Id контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Расчетный счет
        /// </summary>
        public BillSettlementAccountDto SettlementAccount { get; set; }

        /// <summary>
        /// Договор с контрагентом
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Дата окончания действия счета
        /// </summary>
        public DateTime? DeadLine { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Предмет договора (для счета-договора)
        /// </summary>
        public string ContractSubject { get; set; }

        /// <summary>
        /// Тип начисления НДС 1 - не начислять, 2 - сверху, 3 - в том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Статус "Печать и подпись"
        /// </summary>
        public bool IsCovered { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Поступившая оплата
        /// </summary>
        public decimal PaidSum { get; set; }
    }
}