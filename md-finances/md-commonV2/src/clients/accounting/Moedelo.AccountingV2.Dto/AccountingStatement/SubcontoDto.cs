using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccountingV2.Dto.AccountingStatement
{
    public class SubcontoDto
    {
        public object Id { get; set; }

        public long? SubcontoId { get; set; }

        public SubcontoType SubcontoType { get; set; }

        public string SubcontoName { get; set; }
    }
}
