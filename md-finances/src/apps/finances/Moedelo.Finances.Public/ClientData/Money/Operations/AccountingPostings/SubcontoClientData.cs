using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Finances.Public.ClientData.Money.Operations.AccountingPostings
{
    public class SubcontoClientData
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public SubcontoType SubcontoType { get; set; }

        public long SubcontoId { get; set; }

        public bool ReadOnly { get; set; }

        public bool? IsForIp { get; set; }
    }
}