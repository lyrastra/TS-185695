using System.Collections.Generic;

namespace Moedelo.RequisitesV2.Dto.FirmBanks
{
    public class FirmBanksDto
    {
        public int FirmId { get; set; }
        public List<int> BankIds { get; set; }
    }
}