using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.Dss.Dto
{
    public class MobileAuthDto
    {
        public bool IsInstalled { get; set; }
        public string QrCode { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
