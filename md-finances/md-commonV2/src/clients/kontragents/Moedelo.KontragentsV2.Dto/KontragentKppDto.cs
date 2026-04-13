using System;

namespace Moedelo.KontragentsV2.Dto
{
    public class KontragentKppDto
    {
        public long Id { get; set; }
        public int KontragentId { get; set; }
        public string Number { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}