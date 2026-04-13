using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.EdsWizard
{
   public enum EdsWizardScenario
    {
        [Description("выпуск")]
        Request = 10,
        [Description("перевыпуск")]
        ReRequest = 20,
        [Description("продление")]
        Prolongation = 30
    }
}
