using System;
using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.PurchasesUpds.Models
{
    public class PurchasesUpdWithItemsDto
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
        
        public int KontragentId { get; set; }

        public List<PurchasesUpdItemDto> Items { get; set; }
    }
}