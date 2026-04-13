using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesBills.Models
{
    public class SalesBillByCriteriaResponseDto
    {
        public long Id { get; set; }
        
        public string DocumentNumber { get; set; }
        
        public DateTime DocumentDate { get; set; }
        
        public PaidStatus PaidStatus { get; set; }

        public SalesBillKontragentDto Kontragent { get; set; }

        public bool IsCovered { get; set; }
        
        public decimal Sum { get; set; }
        
        public int LinkedDocumentsCount { get; set; }
        
        public bool ReadOnly { get; set; }
    }
}