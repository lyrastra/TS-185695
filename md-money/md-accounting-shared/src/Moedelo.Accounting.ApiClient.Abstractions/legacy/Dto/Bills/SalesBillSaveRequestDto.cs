using Moedelo.Accounting.Enums.Documents;
using System;
using System.Collections.Generic;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Bills
{
    public class SalesBillSaveRequestDto
    {
        /// <summary>
        /// Id документа
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Номер документа
        /// Если поле не заполнено, значение будет вычислено автоматически.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime? DocDate { get; set; }

        /// <summary>
        /// Договор с контрагентом
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Склад
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Id расчетного счета
        /// </summary>
        public int? SettlementAccountId { get; set; }

        /// <summary>
        /// Тип счета 1 - Обычный, 2 - Счет-договор
        /// </summary>
        public BillType Type { get; set; }

        /// <summary>
        /// Статус счета 4 - Неоплачен, 5 - Частично оплачен, 6 - Оплачен
        /// </summary>
        public BillStatus Status { get; set; } = BillStatus.NotPayed;

        /// <summary>
        /// Id контрагента
        /// </summary>
        public int KontragentId { get; set; }

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
        public NdsPositionType? NdsPositionType { get; set; }

        /// <summary>
        /// Статус "Закрыт"
        /// </summary>
        public bool IsCovered { get; set; }

        /// <summary>
        /// Статус "Печать и подпись"
        /// </summary>
        public bool UseStampAndSign { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<SalesBillItemSaveRequestDto> Items { get; set; }
    }
}