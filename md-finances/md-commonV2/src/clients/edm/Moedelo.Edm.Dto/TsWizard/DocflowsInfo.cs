using System.Collections.Generic;

namespace Moedelo.Edm.Dto.TsWizard
{
    /// <summary>
    /// Информация о непрочитанных ДО
    /// </summary>
    public class DocflowsInfoDto
    {
        public List<DocflowInfoItemDto> Docflows { get;  set; }
    }
}
