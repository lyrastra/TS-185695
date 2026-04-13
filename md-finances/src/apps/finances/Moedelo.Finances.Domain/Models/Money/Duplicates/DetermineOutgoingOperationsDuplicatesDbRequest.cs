using System;

namespace Moedelo.Finances.Domain.Models.Money.Duplicates
{
    public class DetermineOutgoingOperationsDuplicatesDbRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SettlementAccountId { get; set; }
    }
}