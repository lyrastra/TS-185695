using Moedelo.Contracts.Enums;

namespace Moedelo.Money.Business.Contracts
{
    internal sealed class Contract
    {
        public int Id { get; set; }

        public long DocumentBaseId { get; set; }

        public int KontragentId { get; set; }

        public ContractKind ContractKind { get; set; }

        public bool IsMainContract { get; set; }

        public long? SubcontoId { get; set; }
    }
}
