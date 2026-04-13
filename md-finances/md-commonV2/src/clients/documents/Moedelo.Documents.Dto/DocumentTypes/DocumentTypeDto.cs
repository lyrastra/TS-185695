using System;

namespace Moedelo.Documents.Dto.DocumentTypes
{
    public class DocumentTypeDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public DocumentBaseTypeDto BaseType { get; set; }
        public DocumentBaseDirectionTypeDto? DirectionType { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }

    }
}
