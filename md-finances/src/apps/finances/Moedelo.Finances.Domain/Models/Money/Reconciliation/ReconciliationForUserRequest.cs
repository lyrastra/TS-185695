using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.Finances.Domain.Models.Money.Reconciliation
{
    public class ReconciliationForUserRequest
    {
        public string FileId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid SessionId { get; set; }
        public string SettlementNumber { get; set; }
        public MovementReviseStatus Status { get; set; }
        public bool IsManual { get; set; }
    }
}
