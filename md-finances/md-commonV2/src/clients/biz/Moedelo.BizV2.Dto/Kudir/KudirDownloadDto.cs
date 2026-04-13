using System.Collections.Generic;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.BizV2.Dto.Kudir
{
    public class KudirDownloadDto
    {
        public int Year { get; set; }

        public DocumentFormat Format { get; set; }

        public List<KudirRowDto> ExtraIncomingPostings { get; set; } = new List<KudirRowDto>();
    }
}
