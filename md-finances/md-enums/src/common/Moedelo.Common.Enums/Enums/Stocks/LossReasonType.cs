using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Stocks
{
    public enum LossReasonType
    {
        [Description("")]
        Default = 0,

        [Description("В пределах норм естественной убыли")]
        InNaturalLossesNorm = 1,

        [Description("Виновное лицо (сотрудник)")]
        GuiltyWorker = 2,

        [Description("Виновное лицо (не сотрудник)")]
        GuiltyNotWorker = 3,

        [Description("Форс-мажор")]
        ForceMajeur = 4,

        [Description("Сверх норм естественной убыли")]
        OverNatuhalLossesNorm = 5,

        [Description("Виновные не найдены")]
        NoGuiltyWorkersFound = 6
    }
}