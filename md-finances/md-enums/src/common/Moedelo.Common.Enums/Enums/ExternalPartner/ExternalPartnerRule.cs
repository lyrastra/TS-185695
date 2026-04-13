namespace Moedelo.Common.Enums.Enums.ExternalPartner
{
    public enum ExternalPartnerRule
    {
        OfficeApi = 0,
        HomeAuthentication = 1,

        CpaNetworkStatSet = 101,
        CpaNetworkStatGet = 102,

        /// <summary>
        /// Область доступная только мобильному приложению на Android
        /// </summary>
        MoedeloMobileAndroidArea = 111,

        /// <summary>
        /// Область доступная только мобильному приложению на iOS
        /// </summary>
        MoedeloMobileIosArea = 112,

        /// <summary>
        /// Отправка sms для подтверждения номера телефона
        /// </summary>
        SendPhoneVerificationSms = 120,

        /// <summary>
        /// Проверка кода, присланного по sms для подтверждения номера телефона
        /// </summary>
        CheckPhoneVerificationSmsCode = 121,

        /// <summary>
        /// Проверка статуса пользователя по логину
        /// </summary>
        CheckUserStatusByLogin = 122,

        /// <summary>
        /// Проверка статуса пользователя по телефону регистрации
        /// </summary>
        CheckUserStatusByRegistrationPhone = 123,

        /// <summary>
        /// Регистрация нового пользователя из мобильного приложения
        /// </summary>
        NewUserRegistrationFromMobile = 150,
        
        /// <summary>
        /// Регистрация нового пользователя только по номеру телефона
        /// </summary>
        NewUserRegistrationByOnlyPhoneNumber = 151,

        ExternalPartnerLeadStat = 200,

        /// <summary>
        /// "Доступ к тестовому методу"
        /// Используется для тестов
        /// </summary>
        TestMethodAccess = 303,
        /// <summary>
        /// "Ни у кого нет такого доступа"
        /// Используется для тестов
        /// </summary>
        NobodyHasThisAccess = 401,

        OfficeContragentAccess = 30001,
        OfficeExcerptAccess = 30002,
        OfficeBlockedAccountsAccess = 30003,
        OfficeRelationsAccess = 30004,
        OfficeArbitrationsAccess = 30005,
        OfficeContragentShortInfoAccess = 30006,
        OfficeContragentOnControl = 30007,
        OfficeContragentHistory = 30008,
        OfficeBbsOrganizationSearch = 30009,
        OfficeBbsGetRegions = 30010,
        OfficeLaunderingScoring = 30011,
        OfficeArbitrationAmountAccess = 30012,
        OfficeTaxesAccess = 30013,
        OfficeContragentMultiCheckAccess = 30014,
        OfficeExecutoryMessagesAccess = 30015,
        OfficeBankruptcyMessagesAccess = 30016,
        OfficePartnerCacheAccess = 30017,
        OfficeOrgInfoAccessDenied = 30105,
        
        OfficeRatingRightAccess = 30101,
        OfficeRatingReputationAccess = 30102,
        OfficeRatingFinanceAccess = 30103,
        OfficeRatingTaxAccess = 30104,
        OfficeRatingArbitrationAccessDenied = 30106,
        OfficeRatingBankruptcyAccessDenied = 30107,

        CalendarApi = 40001,
        UserActivityAnalyticsApi = 40002,

        MindboxApi = 40003,
        IqDialerApi = 40004,
        SalesAiApi = 40005

    }
}
