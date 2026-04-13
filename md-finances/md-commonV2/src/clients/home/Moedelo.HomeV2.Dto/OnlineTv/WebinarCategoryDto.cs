using Moedelo.Common.Enums.Enums.OnlineTv;

namespace Moedelo.HomeV2.Dto.OnlineTv
{
    public class WebinarCategoryDto
    {
        public int Id { get; set; }

        public OnlineTvCategoryType Type { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public int Level { get; set; }

        public string TreePosition { get; set; }
    }
}