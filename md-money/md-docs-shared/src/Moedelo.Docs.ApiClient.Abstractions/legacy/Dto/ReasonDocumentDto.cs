using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Dto
{
    public class ReasonDocumentDto
    {
        public long DocumentId { get; set; }

        public ReasonDocumentType Type { get; set; }
    }
}