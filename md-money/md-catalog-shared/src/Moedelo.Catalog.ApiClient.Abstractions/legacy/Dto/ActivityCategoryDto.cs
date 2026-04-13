using Moedelo.Catalog.ApiClient.Enums;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto
{
    public class ActivityCategoryDto
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public ActivityCategoryVersion Version { get; set; }
    }
}
