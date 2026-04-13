using System;

namespace Moedelo.Money.Providing.Business.Contracts
{
    class Contract
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public long SubcontoId { get; set; }
        public bool IsMainContract { get; set; }
    }
}
