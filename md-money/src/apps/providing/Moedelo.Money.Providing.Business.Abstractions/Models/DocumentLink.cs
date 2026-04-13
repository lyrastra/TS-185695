using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.Money.Providing.Business.Abstractions.Models
{
    public class DocumentLink
    {
        public long DocumentBaseId { get; set; }

        public decimal LinkSum { get; set; }

        public LinkedDocumentType Type { get; set; }
    }
}
