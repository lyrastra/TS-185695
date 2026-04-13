using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto.SendReports
{
    public class SendReportsRequestDto
    {
        public string Code { get; set; }
        public IReadOnlyCollection<int> VersionIds { get; set; }
        public string XReadlIp { get; set; }
    }
}
