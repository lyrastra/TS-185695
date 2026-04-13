using System;

namespace Moedelo.PayrollV2.Dto.Funds
{
    public class FundsRegistryRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPfr { get; set; }
        public bool IsFfoms { get; set; }
        public bool IsFssDisability { get; set; }
        public bool IsFssInjured { get; set; }
        public bool IsSfr { get; set; }
    }
}