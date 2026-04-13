using System.Collections.Generic;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Waybills.Dto
{
    public class PurchasesWaybillDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }
        
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
        /// Провести в бух. учете
        /// </summary>
        public bool ProvideInAccounting { get; set; }
        
        /// <summary>
        /// Компенсируется ли документ заказчиком
        /// </summary>
        public bool IsCompensated { get; set; }

        /// <summary>
        /// Заказчик
        /// </summary>
        public int? CustomerId { get; set; }

        /// <summary>
        /// Посреднический договор
        /// </summary>
        public long? CustomerProjectId { get; set; }

        /// <summary>
        ///  Позиции накладной
        /// </summary>
        public IList<PurchasesWaybillItemDto> Items { get; set; }

        /// <summary>
        /// Счет-фактура
        /// </summary>
        public LinkedInvoiceDto Invoice { get; set; }

        /// <summary>
        /// Информация об изменениях документа
        /// </summary>
        public DocumentContextDto Context { get; set; }

        /// <summary>
        /// Система налогообложения, в которой будет учтён документ (1 - УСНО, 2 - ОСНО, 3 - ЕНВД)
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }
        
        /// <summary>
        /// Наличие несоответствия по количеству/качеству
        /// </summary>
        public bool DiscrepancyNumberOrQuality { get; set; }
    }
}