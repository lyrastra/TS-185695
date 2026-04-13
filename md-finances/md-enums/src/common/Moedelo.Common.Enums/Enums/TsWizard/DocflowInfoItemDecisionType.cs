using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.TsWizard
{
    /// <summary>
    /// Решение по документообороту
    /// </summary>
    public enum DocflowInfoItemDecisionType
    {
        /// <summary>
        /// Обновить статус руками
        /// </summary>
        [Description("Необходимо обновить статус приглашения вручную")]
        RequestOperatorToSetStatusManually,

        /// <summary>
        /// Не удалось принять решение
        /// </summary>
        [Description("Необходимо создать ТС на разработчиков")]
        UnknownSituation,

        [Description("Документ на подписании - всё ОК")]
        OKInSigning,

        [Description("Статус документа в Стеке - ошибка. Создайте ТС")]
        CreateTSTask
    }
}
