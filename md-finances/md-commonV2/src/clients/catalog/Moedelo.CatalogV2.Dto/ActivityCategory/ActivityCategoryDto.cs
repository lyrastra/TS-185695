using Moedelo.Common.Enums.Enums.ActivityCategory;

namespace Moedelo.CatalogV2.Dto.ActivityCategory
{
    public class ActivityCategoryDto
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public Version Version { get; set; }
    }
}