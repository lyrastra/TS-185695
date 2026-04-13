using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto
{
    public class SalesUpdSaveRequestDto
    {
        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }
        
        /// <summary>
        /// Документ для маркетплейса
        /// </summary>
        public MarketplaceType? Marketplace { get; set; }
        
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime? Date { get; set; }
        
        /// <summary>
        /// Тип УПД 1 - Тип 1, 2 - Тип 2
        /// </summary>
        public UpdStatus Status { get; set; }
        
        /// <summary>
        /// В какой системе налогообложения учитывать 1 - УСН, 2 - ОСНО, 3 - ЕНВД
        /// </summary>
        public TaxationSystemType? TaxSystem { get; set; }
        
        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }
        
        /// <summary>
        /// DocumentBaseId договора
        /// </summary>
        public long? ContractId { get; set; }
        
        /// <summary>
        /// DocumentBaseId счета (по счету)
        /// </summary>
        public long? BillId { get; set; }
        
        /// <summary>
        /// Идентификатор грузополучателя
        /// </summary>
        public int? ReceiverId { get; set; }

        /// <summary>
        /// Идентификатор грузоотправителя
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// Идентификатор склада
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Тип начисления НДС
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Позиции документа
        /// </summary>
        public List<SalesUpdItemSaveRequestRestDto> Items { get; set; }

        /// <summary>
        /// Связанные платежи
        /// </summary>
        public List<SalesUpdPaymentSaveRequestRestDto> Payments { get; set; }

        /// <summary>
        /// НДС к вычету
        /// </summary>
        public List<SalesUpdNdsDeductionSaveRequestRestDto> NdsDeductions { get; set; }
        
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
        /// Дата доставки
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        
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