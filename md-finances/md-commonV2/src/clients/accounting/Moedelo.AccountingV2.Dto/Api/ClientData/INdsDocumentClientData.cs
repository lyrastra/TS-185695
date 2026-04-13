using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public interface INdsDocumentClientData
    {
        decimal Count { get; set; }

        decimal Price { get; set; }

        decimal SumWithoutNds { get; set; }

        decimal SumWithNds { get; set; }

        decimal NdsSum { get; set; }

        bool IsCustomSums { get; set; }

        NdsType NdsType { get; set; }
    }
}