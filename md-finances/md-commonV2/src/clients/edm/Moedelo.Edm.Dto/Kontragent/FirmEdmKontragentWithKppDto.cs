using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.Edm.Dto.Kontragent
{
    public class FirmEdmKontragentWithKppDto : FirmEdmKontragentDto
    {
        public string Kpp { get; set; }
        public string StatusString { get; set; }
        public bool IsActive { get; set; }
    }
}
