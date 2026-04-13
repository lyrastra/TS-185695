using System;

namespace Moedelo.Finances.Dto.Money.Duplicates
{
    public class DuplicateMaterialAidOperationRequestDto
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public int SettlementAccountId { get; set; }
        public int KontragentId { get; set; }
    }
}