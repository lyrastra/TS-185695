using System;
using System.Collections.Generic;

namespace Moedelo.UserLoginLog.Dto
{
    public class UserLoginSummaryRequestDto
    {
        public IReadOnlyCollection<string> Platforms { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
