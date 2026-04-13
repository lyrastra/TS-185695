using System;
using Moedelo.Docs.ApiClient.Abstractions.Common.Models;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesStatements.Models
{
    public class DocsPurchasesStatementByCriteriaResponseDto
    {
        /// <summary>
        /// Идентификатор акта
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Номер акта
        /// </summary>
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// Дата акта
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
        /// Сумма акта
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