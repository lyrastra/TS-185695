using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.Finances.Client.Money.Dtos
{
    public class ReconciliationForBackofficeRequestDto
    {
        public int FirmId { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FileId { get; set; }
        public string SettlementNumber { get; set; }
        public MovementReviseStatus Status { get; set; }
    }
}
