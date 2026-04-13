using System;

namespace Moedelo.Finances.Business.Services.Integrations.Models
{
    internal class OrderWithGuid
    {
        public Guid Guid { get; set; }
        public long DocumentBaseId { get; set; }
        public int SettlementAccountId { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string Number { get; set; }
        public object Order { get; set; }
    }
}
