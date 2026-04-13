using System;

namespace Moedelo.Money.Providing.Business.Estate.Models
{
    class ReceiptStatement
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }
    }
}
