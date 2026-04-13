using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.LinkedDocuments
{
    public class DocumentLinkSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public decimal LinkSum { get; set; }
    }
}