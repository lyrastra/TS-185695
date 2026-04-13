using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Auth;

namespace Moedelo.UserLoginLog.Dto
{
    public class LastLoginDatesRequestDto
    {
        public IEnumerable<int> UserIds { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}