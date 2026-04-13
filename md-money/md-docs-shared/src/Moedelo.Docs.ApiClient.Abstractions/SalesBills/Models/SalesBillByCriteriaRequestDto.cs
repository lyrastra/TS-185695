using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesBills.Models
{
    public class SalesBillByCriteriaRequestDto
    {
        public int Offset { get; set; } = 0;
        
        public int Limit { get; set; } = 20;

        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public string Query { get; set; }
        
        public string ItemQuery { get; set; }
        
        public HashSet<int> KontragentIds { get; set; } = null;
        
        public HashSet<long> ContractIds { get; set; } = null;
        
        public decimal? MinDocumentSum { get; set; }
        
        public decimal? MaxDocumentSum { get; set; }
        
        public bool? IsCovered { get; set; } = null;
        
        public bool? IsExpired { get; set; } = null;
        
        public bool? IsScheduleSource { get; set; } = null;
        
        public bool? IsScheduleCopy { get; set; } = null;
        
        public HashSet<PaidStatus> PaidStatuses { get; set; } = null;
        
        public string OrderBy { get; set; } = "asc";
        
        public string SortBy { get; set; } = "DocumentDate";
    }
}