using System;
using Moedelo.Docs.ApiClient.Abstractions.Common.Models;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesInvoices
{
    public class DocsSalesInvoiceByCriteriaResponseDto
    {
        /// <summary>
        /// Идентификатор счета-фактуры
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Номер счета-фактуры
        /// </summary>
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// Дата счета-фактуры
        /// </summary>
        public DateTime DocumentDate { get; set; }
        
        /// <summary>
        /// Признак: "Подписан"
        /// </summary>
        public SignStatus SignStatus { get; set; }
        
        /// <summary>
        /// Контрагент
        /// </summary>
        public DocsKontragentDto Kontragent { get; set; }

        /// <summary>
        /// Признак: проведен в бухучете
        /// </summary>
        public bool ProvideInAccounting { get; set; }
        
        /// <summary>
        /// Сумма счета-фактуры
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
        /// Причина, по которой нельзя редактировать документ 
        /// </summary>
        public ReadOnlyReasonType? ReadOnlyReason { get; set; }
        
        /// <summary>
        /// Тип счета-фактуры
        /// </summary>
        public InvoiceType Type { get; set; }
        
        /// <summary>
        /// Откуда создан документ
        /// 1 - из остатков
        /// 2 - из имущества
        /// 3 - из накладной
        /// 4 - из акта
        /// </summary>
        public CreationPlace CreatedFrom { get; set; }
    }
}