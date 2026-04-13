using Moedelo.Common.Enums.Enums.EdsWizard;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.Eds
{
    public class EdsWizardStateDto
    {
        public EdsProvider EdsProvider { get; set; }
        public EdsState EdsState { get; set; }
        public EdsWizardScenario EdsWizardScenario { get; set; }
        public bool? NeedPfrSignature { get; set; }
        public bool HasMajorRequisitesChanges { get; set; }

        public EdsTransferType TransferType { get; set; }
        public EdsTransferInfo TransferInfo { get; set; }
        public bool IsTransfer => TransferInfo != null;
    }
}
