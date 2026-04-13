using System;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.EdsSmev
{
    public class EdsSmevFailure
    {
        public int FirmId { get; set; }
        public DateTime Date { get; set; }
        public EdsProvider EdsProvider { get; set; }
        public string ScenarioName { get; set; }
        public string Inn { get; set; }
        public string Login { get; set; }
        public string OrgName { get; set; }
        public string ProblemReason { get; set; }
        public SignatureStatus SignatureStatus { get; set; }
    }
}