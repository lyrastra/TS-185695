using Moedelo.Common.Enums.Enums.Sps;

namespace Moedelo.SpsV2.Dto.Rubrics
{
    public class RubricInfoRequestDto
    {
        public RubricType Type { get; set; }

        public int? ParentRubricId { get; set; }
    }
}
