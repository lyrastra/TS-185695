using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.Finances.Client.Money.Dtos
{
    public class ReconciliationForUserRequestDto
    {
        public int FirmId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FileId { get; set; }
        public Guid SessionId { get; set; }
        public string SettlementNumber { get; set; }
        public MovementReviseStatus Status { get; set; }
        public bool IsManual { get; set; }
    }
}
