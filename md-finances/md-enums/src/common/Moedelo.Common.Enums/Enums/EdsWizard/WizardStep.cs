using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.EdsWizard
{
    public enum WizardStep
    {
        [Description("Не определено")]
        None,

        [Description("Получение и подключение ЭП")]
        Initial,

        [Description("Выбор сертификата ЭП")]
        CertificateSelection,

        [Description("Список ведомств")]
        Funds,

        [Description("Регистрация в ведомствах")]
        CertificateRegistration,

    }
}