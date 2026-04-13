using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesBills.Models
{
    public class SalesBillDto
    {
        public int Id { get; set; }

        public long DocumentBaseId { get; set; }
        
        public string DocumentNumber { get; set; }
        
        public DateTime DocumentDate { get; set; }
        
        public int KontragentId { get; set; }
        
        public bool IsCovered { get; set; }
        
        public decimal Sum { get; set; }
        
        public PaidStatus PaidStatus { get; set; }
        
        public BillType BillType { get; set; }

        public bool UseStampAndSign { get; set; }
        
        public NdsPositionType? NdsPositionType { get; set; }
        
        public string AdditionalInfo { get; set; }
        
        public DateTime DeadTimeForBill { get; set; }
        
        public string ContractSubject { get; set; }
        
        public int SettlementAccountId { get; set; }

        public bool UseNds { get; set; }
    }
}