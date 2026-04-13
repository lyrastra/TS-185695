using System.Collections.Generic;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.BizV2.Dto.Kudir
{
    public class KudirDownloadFilesDto
    {
        public int Year { get; set; }

        public DocumentFormat[] Formats { get; set; }

        public List<KudirRowDto> ExtraIncomingPostings { get; set; } = new List<KudirRowDto>();
    }
}
