using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using System;

namespace Moedelo.AccountingV2.Dto.Waybills.Sales
{
    public class SalesWaybillCollectionItemDto
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
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Тип начисления НДС 1 - не начислять, 2 - сверху, 3 - в том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Id грузоотправителя
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// Id поставщика
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// Id грузополучателя
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Id плательщика
        /// </summary>
        public int? PayerId { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public WayBillTypesEnum Type { get; set; }

        /// <summary>
        /// Id контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Id связанного счета
        /// </summary>
        public long? BillId { get; set; }

        /// <summary>
        /// Id связанного договора
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Id склада с которого происходит продажа
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Признак документа "Подписан" (Да, Нет, Скан)
        /// </summary>
        public string OnHands { get; set; }

        /// <summary>
        /// Учитывается в системе налогообложения
        /// 1 - УСН, 2 - ОСНО, 3 - ЕНВД
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}