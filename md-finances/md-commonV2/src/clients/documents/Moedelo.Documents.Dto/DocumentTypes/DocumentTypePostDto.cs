namespace Moedelo.Documents.Dto.DocumentTypes
{
    public class DocumentTypePostDto
    {
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public DocumentBaseTypeDto BaseType { get; set; }
        public DocumentBaseDirectionTypeDto? DirectionType { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }

}
