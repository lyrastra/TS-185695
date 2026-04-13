using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto
{
    /// <summary>
    /// Модель УПД продажи, используемая в private API
    /// </summary>
    public class SalesUpdDto
    {
        public int Id { get; set; }
        
        public long DocumentBaseId { get; set; }

        public int FirmId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

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
        
        public bool IsForgottenDocument { get; set; }
        
        /// <summary>
        /// Дата забытого документа
        /// </summary>
        public DateTime? ForgottenDocumentDate { get; set; }

        /// <summary>
        /// Номер забытого документа
        /// </summary>
        public string ForgottenDocumentNumber { get; set; }
        
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
    }
}