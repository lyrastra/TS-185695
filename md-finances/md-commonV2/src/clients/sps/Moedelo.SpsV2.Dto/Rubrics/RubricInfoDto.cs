using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Sps;
using Moedelo.SpsV2.Dto.Documents;

namespace Moedelo.SpsV2.Dto.Rubrics
{
    public class RubricInfoDto
    {
        public RubricType RubricType { get; set; }

        public int? ParentRubric { get; set; }

        public string ParentRubricFullName { get; set; }

        public List<RubricNameDto> ChildRubrics { get; set; }

        public List<DocNameDto> DocumentsInfo { get; set; }
    }
}
