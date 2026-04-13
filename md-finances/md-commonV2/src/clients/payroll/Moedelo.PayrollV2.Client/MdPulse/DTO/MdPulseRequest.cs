using System.Collections.Generic;

namespace Moedelo.PayrollV2.Client.MdPulse.DTO
{
    public class MdPulseRequest
    {
        public IEnumerable<int> FirmIds { get; set; }
        public IEnumerable<string> RegionCodes { get; set; }
    }
}