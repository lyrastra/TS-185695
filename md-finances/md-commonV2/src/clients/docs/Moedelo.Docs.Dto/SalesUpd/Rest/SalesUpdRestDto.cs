using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Marketplaces;

namespace Moedelo.Docs.Dto.SalesUpd.Rest
{
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
        /// Номер документа основания для контрагента Wildberries
        /// </summary>
        public string RansomNoticeNumber { get; set; }
        
        /// <summary>
        /// Дата документа основания для контрагента Wildberries
        /// </summary>
        public DateTime? RansomNoticeDate { get; set; }
        
        /// <summary>
        /// Наименование документа основания для контрагента Wildberries
        /// </summary>
        public string RansomNoticeName { get; set; }

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
        /// Идентификаторы связанных счетов
        /// </summary>
        public List<long> BillIds { get; set; }

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
        public List<SalesUpdItemRestDto> Items { get; set; }

        /// <summary>
        /// Склад, с которого списаны товары
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Связанные платежи
        /// </summary>
        public List<SalesUpdPaymentRestDto> Payments { get; set; }

        /// <summary>
        /// Вычеты НДС по связанным авансовым счетам-фактурам
        /// </summary>
        public List<SalesUpdNdsDeductionRestDto> NdsDeductions { get; set; }

        /// <summary>
        /// Тип маркетплейса, например: озон
        /// </summary>
        public MarketplaceType? MarketplaceType { get; set; }

        /// <summary>
        /// Иные сведения об отгрузке, передаче
        /// </summary>
        public string ShipmentDetail { get; set; }

        /// <summary>
        /// Дата доставки
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        
        /// <summary>
        /// Добавить печать и подпись
        /// </summary>
        public bool UseStampAndSign { get; set; }

        /// <summary>
        /// Тип УПД на продажу
        /// </summary>
        public UpdSaleType? SaleType { get; set; }
        
        /// <summary>
        /// Иные сведения об отгрузке, передаче
        /// </summary>
        public string OtherShipmentDetails { get; set; }
        
        /// <summary>
        /// Время доставки с..
        /// </summary>
        public string DeliveryTimeFrom { get; set; }

        /// <summary>
        /// Время доставки ..до
        /// </summary>
        public string DeliveryTimeTo { get; set; }

        /// <summary>
        /// Идентификатор госконтракта (ИГК)
        /// </summary>
        public string GovernmentContractId { get; set; }

        /// <summary>
        /// Данные о транспортировке и грузе
        /// </summary>
        public string TransportationDetails { get; set; }
    }
}