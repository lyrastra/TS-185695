using System;
using Moedelo.Contracts.Enums;

namespace Moedelo.Money.Domain.LinkedDocuments
{
    public class ContractLink
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public ContractKind ContractKind { get; set; }
    }
}
