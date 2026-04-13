using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.EReportRequisites
{
    public class EReportRequisitesDto
    {
        public SignatureStatus? SignatureStatus { get; set; }

        public string PhoneNumber { get; set; }

        public List<FnsData> EReportFnsList { get; set; }
    }
}
