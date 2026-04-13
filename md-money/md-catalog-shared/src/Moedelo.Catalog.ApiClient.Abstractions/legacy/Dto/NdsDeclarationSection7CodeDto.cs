using Moedelo.Catalog.ApiClient.Enums;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto
{
    public class NdsDeclarationSection7CodeDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public NdsDeclarationSection7ArticleNumber ArticleNumber { get; set; }
    }
}