using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using System;

namespace Moedelo.AccountingV2.Dto.Waybills.Purchases
{
    public class PurchasesWaybillCollectionItemDto
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
        /// Тип
        /// </summary>
        public WayBillTypesEnum Type { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Договор с контрагентом
        /// </summary>
        public long? ProjectId { get; set; }

        /// <summary>
        /// Тип начисления НДС 0 - Нет, 1 - Сверху, 2 - В том числе
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Сумма документа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Id склада, если товар/материал оприходован на склад
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Флаг показывающий проводку документа
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Наличие несоответствия по количеству/качеству
        /// </summary>
        public bool DiscrepancyNumberOrQuality { get; set; }

        /// <summary>
        /// Учитывается в системе налогообложения
        /// 1 - УСН, 2 - ОСНО, 3 - ЕНВД
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}