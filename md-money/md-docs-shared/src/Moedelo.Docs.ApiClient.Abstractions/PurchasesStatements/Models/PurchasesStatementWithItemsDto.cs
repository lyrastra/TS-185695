using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesStatements.Models
{
    public class PurchasesStatementsWithItemsDto
    {
        /// <summary>
        /// Идентификатор УПД
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Номер УПД
        /// </summary>
        public string DocumentNumber { get; set; }
        
        /// <summary>
        /// Дата УПД
        /// </summary>
        public DateTime DocumentDate { get; set; }
        
        /// <summary>
        /// Идентификатор (не BaseId) договора 
        /// </summary>
        public int? ContractId { get; set; }
        
        public int KontragentId { get; set; }

        public List<PurchasesStatementsItemDto> Items { get; set; }
    }
}