using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Marketplaces;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using NdsPositionType = Moedelo.Common.Enums.Enums.Documents.NdsPositionType;
using TaxationSystemType = Moedelo.Common.Enums.Enums.Accounting.TaxationSystemType;
using UpdSaleType = Moedelo.Common.Enums.Enums.Documents.UpdSaleType;
using UpdStatus = Moedelo.Common.Enums.Enums.Documents.UpdStatus;

namespace Moedelo.Docs.Dto.SalesUpd.Rest
{
    public class SalesUpdCreateRequestRestDto
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
        /// Идентификатор грузополучателя
        /// </summary>
        public int? ReceiverId { get; set; }
        
        /// <summary>
        /// Идентификатор гузоотправителя
        /// </summary>
        public int? SenderId { get; set; }
        
        /// <summary>
        /// DocumentBaseId договора
        /// </summary>
        public long? ContractId { get; set; }
        
        /// <summary>
        /// DocumentBaseId счета (по счету)
        /// </summary>
        public long? BillId { get; set; }

        /// <summary>
        /// Идентификаторы связанных счетов
        /// </summary>
        public List<long> BillIds { get; set; }

        /// <summary>
        /// Идентификатор склада
        /// </summary>
        public int? StockId { get; set; }

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

        /// <summary>
        /// Документ прошлого периода
        /// </summary>
        public bool IsForgottenDocument { get; set; }

        /// <summary>
        /// Дата документа прошлого периода
        /// </summary>
        public DateTime? ForgottenDocumentDate { get; set; }

        /// <summary>
        /// Номер документа прошлого периода
        /// </summary>
        public string ForgottenDocumentNumber { get; set; }

        /// <summary>
        /// Вкладка Учёт: флаг "Обычный вид деятельности" (если есть позиции с услугами)
        /// </summary>
        public bool? IsMainActivity { get; set; }

        /// <summary>
        /// Вкладка Учёт: счёт контрагента
        /// </summary>
        public SyntheticAccountCode? KontragentAccountCode { get; set; }
    }
}