using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.CashV2.Dto.Evotor
{
    public class EvotorIntegrationDto
    {
        public int FirmId { get; set; }

        public string EvotorUserId { get; set; }

        public string EvotorAccessToken { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
