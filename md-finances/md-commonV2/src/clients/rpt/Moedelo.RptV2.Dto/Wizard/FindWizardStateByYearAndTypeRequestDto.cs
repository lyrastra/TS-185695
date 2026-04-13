using Moedelo.Common.Enums.Enums.Wizard;

namespace Moedelo.RptV2.Dto.Wizard
{
    public class FindWizardStateByYearAndTypeRequestDto
    {
        public int FirmId { get;  set; }
        public WizardType Type { get; set; }
        public int Year { get; set; }
    }
}
