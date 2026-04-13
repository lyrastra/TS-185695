using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto.SendReports
{
    public class SendCodeResponseDto
    {
        public IReadOnlyCollection<DocumentVersionResponseDto> DocumentVersions { get; set; }

        public string Message { get; set; }
    }
}
