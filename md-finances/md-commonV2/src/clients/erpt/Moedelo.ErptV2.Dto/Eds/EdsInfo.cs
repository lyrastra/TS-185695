using System;
using Moedelo.Common.Enums.Enums.EdsWizard;
using Moedelo.Common.Enums.Enums.ElectronicReports;

namespace Moedelo.ErptV2.Dto.Eds
{
    public class EdsInfo
    {
        public EdsState EdsState { get; set; }
        public EdsWizardScenario EdsWizardScenario { get; set; }
        public EdsExpiry EdsExpiry { get; set; }
        public CertificateSignedInfo CertificateSignedInfo { get; set; }
        public ConfirmationMethod ConfirmationMethod { get; set; }
        public bool EdsExists { get; set; }
        
        [Obsolete("Не актуально. необходимо использовать EdsExists.")]
        // todo заиспользовать в edm EdsExists и выпилить это поле 
        public bool EdsComplete { get; set; }
        public bool NeedAutomaticallyPassStatementStep { get; set; }
        public EdsProvider LastStateEdsProvider { get; set; }
        public bool IsEdsIdentificationKind { get; set; }
    }
}
