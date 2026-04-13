using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.AccountV2.Dto.User
{
    public class UserFirmNamesDto
    {
        public int UserId { get; set; }

        public ISet<FirmNameDto> FirmNames { get; set; }
    }
}
