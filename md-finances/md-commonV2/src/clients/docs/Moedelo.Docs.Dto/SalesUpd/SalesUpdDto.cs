using System;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Common.Enums.Enums.Marketplaces;

namespace Moedelo.Docs.Dto.SalesUpd
{
    public class SalesUpdDto
    {
        public int Id { get; set; }
        
        public long DocumentBaseId { get; set; }

        public int FirmId { get; set; }

        public string Number { get; set; }

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

        public int KontragentId { get; set; }

        public int? KontragentAccountCode { get; set; }

        /// <summary>
        /// Грузоотправитель 
        /// </summary>
        public int? SenderId { get; set; }

        /// <summary>
        /// Грузополучатель
        /// </summary>
        public int? ReceiverId { get; set; }

        public AccountingDocumentType DocumentType => AccountingDocumentType.SalesUpd;

        public UpdStatus Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }

        public bool UseNds => NdsPositionType != NdsPositionType.None;

        public NdsPositionType NdsPositionType { get; set; }

        public TaxationSystemType TaxSystem { get; set; }
        
        public bool ProvideInAccounting { get; set; }

        public ProvidePostingType TaxPostingType { get; set; }

        public long SubcontoId { get; set; }

        public decimal Sum { get; set; }

        public bool UseStampAndSign { get; set; }

        public bool IsForgottenDocument { get; set; }

        public DateTime? ForgottenDocumentDate { get; set; }

        public string ForgottenDocumentNumber { get; set; }

        /// <summary>
        /// Иные сведения об отгрузке, передаче
        /// </summary>
        public string ShipmentDetail { get; set; }

        /// <summary>
        /// Документ для маркетплейса
        /// </summary>
        public MarketplaceType? MarketplaceType { get; set; }

        /// <summary>
        /// Дата доставки
        /// </summary>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// Время доставки с..
        /// </summary>
        public TimeSpan? DeliveryStartTime { get; set; }

        /// <summary>
        /// Время доставки ..до
        /// </summary>
        public TimeSpan? DeliveryEndTime { get; set; }

        public long? StockId { get; set; }

        /// <summary>
        /// Значения свойства "Подписан"
        /// </summary>
        public SignStatus SignStatus { get; set; }

        /// <summary>
        /// Тип УПД на продажу
        /// </summary>
        public UpdSaleType? SaleType { get; set; }

        /// <summary>
        /// Идентификатор госконтракта (ИГК)
        /// </summary>
        public string GovernmentContractId { get; set; }

        /// <summary>
        /// Данные о транспортировке и грузе
        /// </summary>
        public string TransportationDetails { get; set; }

        /// <summary>
        /// Вкладка Учёт: флаг "Обычный вид деятельности" (если есть позиции с услугами)
        /// </summary>
        public bool? IsMainActivity { get; set; }
    }
}