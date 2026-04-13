using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moedelo.SuiteCrm.Dto.Activity
{
    public class UpdateActivityLeadLoginCountDto
    {
        public int UserId { get; set; }

        public int FirmId { get; set; }

        public int LoginCount { get; set; }
    }
}
