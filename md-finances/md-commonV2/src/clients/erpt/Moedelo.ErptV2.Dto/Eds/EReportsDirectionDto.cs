using Moedelo.Common.Enums.Enums.ElectronicReports;
using System.Collections.Generic;

namespace Moedelo.ErptV2.Dto.Eds
{
    public class EReportsDirectionDto
    {
        public FundType FundType { get; set; }

        public string Code { get; set; }

        public List<string> AdditionalInfo { get; set; }
    }
}
