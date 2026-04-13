namespace Moedelo.Categories.Dto.CategoryType
{
    public class CategoryTypePostDto
    {
        public string Name { get; set; }

        public int AccountId { get; set; }

        public CategoryTypePostDto(string name, int accountId)
        {
            Name = name;
            AccountId = accountId;
        }
    }
}
