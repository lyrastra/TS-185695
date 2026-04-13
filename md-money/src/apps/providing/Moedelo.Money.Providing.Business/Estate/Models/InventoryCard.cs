using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Providing.Business.Estate.Models
{
    class InventoryCard
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Cost { get; set; }

        public decimal? PaidCost { get; set; }

        public DateTime? CommissioningDate { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}
