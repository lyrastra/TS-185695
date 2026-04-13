using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.Flc
{
    public class FlcDataDto
    {
        public List<FileDataInfoDto> FileDataInfo { get; set; } = new List<FileDataInfoDto>();

        public FundType FundType { get; set; }
    }
}
