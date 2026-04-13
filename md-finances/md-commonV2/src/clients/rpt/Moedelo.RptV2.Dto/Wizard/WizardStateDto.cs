using Moedelo.Common.Enums.Enums.Wizard;

namespace Moedelo.RptV2.Dto.Wizard
{
    public class WizardStateDto
    {
        public long Id { get; set; }

        public WizardType WizardType { get; set; }

        public int PeriodYear { get; set; }

        public int PeriodNumber { get; set; }

        public int Step { get; set; }

        public bool IsCompleted { get; set; }

        public int Number { get; set; }
    }
}
