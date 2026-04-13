using System.Collections.Generic;

namespace Moedelo.SpsV2.Dto.Forms
{
    public class FormWidgetRequestDto
    {
        public int LastViewCount { get; set; }

        public List<FormRubricType> RubricTypeList { get; set; }
    }
}