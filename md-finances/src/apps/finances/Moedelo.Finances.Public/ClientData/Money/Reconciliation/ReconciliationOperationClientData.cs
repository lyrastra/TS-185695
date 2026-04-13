using System;

namespace Moedelo.Finances.Public.ClientData.Money.Reconciliation
{
    public class ReconciliationOperationClientData
    {
        public long Id { get; set; }
        public bool IsOutgoing { get; set; }
        public bool IsSalary { get; set; }
        public decimal Sum { get; set; }
        public DateTime Date { get; set; }
        public string KontragentName { get; set; }
        public string Description { get; set; }
    }
}