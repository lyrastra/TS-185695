namespace Moedelo.Common.Enums.Flags;

public static class FirmFlags
{
    public const string IsNewBizMainVisited = "IsNewBizMainVisited";
    public const string TaxationSystemInvalidated = "TaxationSystemInvalidated";

    public const string RegisteredFromEvotorFlag = "RegisteredFromEvotorFlag";

    public const string IsEdmActive = "IsEdmActive";

    /// <summary> Состояние чата при загрузке страницы</summary>
    public const string IsBotChatOpen = "IsBotChatOpen";

    public const string IsFromSberbankEds = "IsFromSberbankEds";

    /// <summary> Состояние прохождения Onboarding по заполнению реквизитов</summary>
    public const string IsOnboardingRequisitesCompleted = "IsOnboardingRequisitesCompleted";

    /// <summary> Первый вход пользователя в сервис. Выставляется на Главной</summary>
    public const string FirstUserLogin = "FirstUserLogin";
}