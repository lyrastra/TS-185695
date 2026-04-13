using System;

namespace Moedelo.Documents.Dto.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
