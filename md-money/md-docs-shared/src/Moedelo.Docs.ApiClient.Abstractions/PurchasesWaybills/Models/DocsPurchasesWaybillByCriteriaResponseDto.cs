using System;
using Moedelo.Docs.ApiClient.Abstractions.Common.Models;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesWaybills.Models
{
    public class DocsPurchasesWaybillByCriteriaResponseDto
    {
        /// <summary>
        /// Идентификатор накладной
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Номер накладной
        /// </summary>
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// Дата накладной
        /// </summary>
        public DateTime DocumentDate { get; set; }
        
        /// <summary>
        /// Контрагент
        /// </summary>
        public DocsKontragentDto Kontragent { get; set; }

        /// <summary>
        /// Признак: проведен в бухучете
        /// </summary>
        public bool ProvideInAccounting { get; set; }
        
        /// <summary>
        /// Сумма накладной
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// Кол-во связанных документов
        /// </summary>
        public int LinkedDocumentsCount  { get; set; }
        
        /// <summary>
        /// Признак: только для чтения
        /// </summary>
        public bool ReadOnly { get; set; }
        
        /// <summary>
        /// Причина, по которой документ нельзя редактировать (если установлен флаг ReadOnly)
        /// </summary>
        public ReadOnlyReasonType? ReadOnlyReason { get; set; }
        
        /// <summary>
        /// Тип накладной
        /// </summary>
        public WaybillTypeCode Type { get; set; }
        
        /// <summary>
        /// Статус налогового учета
        /// </summary>
        public TaxStatus TaxStatus { get; set; }
    }
}