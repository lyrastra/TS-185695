using System;
using Moedelo.Docs.ApiClient.Abstractions.Common.Models;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesUpds.Models
{
    public class DocsSalesUpdByCriteriaResponseDto
    {
        /// <summary>
        /// Идентификатор УПД
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Номер УПД
        /// </summary>
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// Дата УПД
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
        /// Сумма УПД
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
        /// Статус налогового учета
        /// </summary>
        public TaxStatus TaxStatus { get; set; }
    }
}