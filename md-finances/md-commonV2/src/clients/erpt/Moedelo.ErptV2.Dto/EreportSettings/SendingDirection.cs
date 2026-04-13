using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.EreportSettings
{
    public class SendingDirection
    {
        public FundType Fund { get; set; }
        public string Code { get; set; }
        public List<string> Routes { get; set; }
    }
}
