using System;

namespace Moedelo.Finances.Domain.Models.Integrations
{
    public class StatementRequestByPurse
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long KontragentId { get; set; }
    }
}
