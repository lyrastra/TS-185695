using Moedelo.Common.Enums.Enums.Bills;
using Moedelo.Common.Enums.Enums.Nds;

namespace Moedelo.AccountingV2.Dto.Statements
{
    public class StatementItemDto
    {
        public string Name { get; set; }

        public decimal Count { get; set; }

        public string Unit { get; set; }

        public string OKEI { get; set; }

        public ItemType Type { get; set; }

        public decimal Price { get; set; }

        public NdsTypes NdsType { get; set; }

        public decimal SumWithoutNds { get; set; }

        public decimal NdsSum { get; set; }

        public decimal SumWithNds { get; set; }

        public virtual int? ActivityAccountCode { get; set; }

        /// <summary>
        /// Позиция с прочерками в ед. изм., кол-ве и цене (только услуги)
        /// </summary>
        public bool IsUnmeasurable { get; set; }
    }
}
