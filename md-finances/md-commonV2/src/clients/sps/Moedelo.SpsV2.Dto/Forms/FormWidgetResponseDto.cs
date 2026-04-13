using System.Collections.Generic;
using Moedelo.SpsV2.Dto.Rubrics;
using Moedelo.SpsV2.Dto.Statistics;

namespace Moedelo.SpsV2.Dto.Forms
{
    public class FormWidgetResponseDto
    {
        public List<RubricDocumentCountDto> RubricDocumentCountList { get; set; }

        public List<LastViewDto> LastViewList { get; set; }
    }
}