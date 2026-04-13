namespace Moedelo.Documents.Dto.Categories
{
    public class CategoryPostDto
    {
        public int AccountId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
