using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.Flc
{
    public class FileDataDto
    {
        public int FirmId { get; set; }
        
        public int? EtrustId { get; set; }

        public FundType FundType { get; set; }

        public List<FileDataInfoDto> Files { get; set; }
    }
}