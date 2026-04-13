namespace Moedelo.Common.AccessRules.Abstractions
{
    public enum AccessRule
    {
        #region Клиентская часть (от 1000 до 2000)

        /// <summary> Логин в личный кабинет </summary>
        AccessMainLogin = 1000,

        /// <summary> Просмотр реквизитов </summary>
        ViewFirmDetails = 1001,

        /// <summary> Редактирование реквизитов </summary>
        EditFirmDetails = 1002,

        /// <summary> Просмотр зарплаты </summary>
        ViewSalary = 1003,

        /// <summary> Редактирование зарплаты </summary>
        EditSalary = 1004,

        /// <summary> Просмотр календаря </summary>
        ViewCalendar = 1005,

        /// <summary> Редактирование календаря </summary>
        EditCalendar = 1059,

        /// <summary> Редактирование отчетности </summary>
        EditDocuments = 1006,

        /// <summary> Просмотр денег </summary>
        ViewMoney = 1007,

        /// <summary> Просмотр денег подчиненных пользователей </summary>
        ViewMoneyBySellers = 1008,

        /// <summary> Просмотр собственных денег </summary>
        ViewMoneyPersonal = 1009,

        /// <summary> Редактирование денег </summary>
        EditMoney = 1010,

        /// <summary> Редактирование денег подчиненных пользователей </summary>
        EditMoneyBySellers = 1011,

        /// <summary> Редактирование собственных денег </summary>
        EditMoneyPersonal = 1012,

        /// <summary> Просмотр договоров </summary>
        ViewProjects = 1013,

        /// <summary> Просмотр договоров подчиненных пользователей </summary>
        ViewProjectsBySellers = 1014,

        /// <summary> Просмотр собственных договоров </summary>
        ViewProjectsPersonal = 1015,

        /// <summary> Редактирование договоров </summary>
        EditProjects = 1016,

        /// <summary> Редактирование договоров подчиненных пользователей </summary>
        EditProjectsBySellers = 1017,

        /// <summary> Редактирование собственных договоров </summary>
        EditProjectsPersonal = 1018,

        /// <summary> Просмотр счетов </summary>
        ViewBills = 1019,

        /// <summary> Просмотр счетов подчиненных пользователей </summary>
        ViewBillsBySellers = 1020,

        /// <summary> Просмотр собственных счетов </summary>
        ViewBillsPersonal = 1021,

        /// <summary> Редактирование счетов </summary>
        EditBills = 1022,

        /// <summary> Редактирование счетов подчиненных пользователей </summary>
        EditBillsBySellers = 1023,

        /// <summary> Редактирование собственных счетов </summary>
        EditBillsPersonal = 1024,

        /// <summary> Просмотр актов </summary>
        ViewStatements = 1025,

        /// <summary> Просмотр актов подчиненных пользователей </summary>
        ViewStatementsBySellers = 1026,

        /// <summary> Просмотр собственных актов </summary>
        ViewStatementsPersonal = 1027,

        /// <summary> Редактирование актов </summary>
        EditStatements = 1028,

        /// <summary> Редактирование актов подчиненных пользователей </summary>
        EditStatementsBySellers = 1029,

        /// <summary> Редактирование собственных актов </summary>
        EditStatementsPersonal = 1030,

        /// <summary> Просмотр контрагентов </summary>
        ViewKontragents = 1031,

        /// <summary> Просмотр контрагентов подчиненных пользователей </summary>
        ViewKontragentsBySellers = 1032,

        /// <summary> Просмотр собственных контрагентов </summary>
        ViewKontragentsPersonal = 1033,

        /// <summary> Редактирование контрагентов </summary>
        EditKontragents = 1034,

        /// <summary> Редактирование контрагентов подчиненных пользователей </summary>
        EditKontragentsBySellers = 1035,

        /// <summary> Редактирование собственных контрагентов </summary>
        EditKontragentsPersonal = 1036,

        /// <summary> Просмотр личных данных сотрудников </summary>
        ViewWorkers = 1037,

        /// <summary> Доступ к многопользовательскому функционалу </summary>
        AccessMultiusers = 1038,

        /// <summary> Редактирование личных данных сотрудников </summary>
        EditWorkers = 1039,

        /// <summary> Настройка отчётности </summary>
        SetDocuments = 1040,

        /// <summary> Основной пользователь </summary>
        MainUser = 1041,

        /// <summary> Загрузка файлов </summary>
        FileUpload = 1042,

        /// <summary> Редактирование личных данных единственного сотрудника </summary>
        EditOnceWorker = 1043,

        /// <summary> Просмотр личных данных единственного сотрудника </summary>
        ViewOnceWorker = 1044,

        /// <summary> Просмотр актов сверки </summary>
        ViewVerificationStatements = 1045,

        /// <summary> Просмотр актов сверки подчиненных пользователей </summary>
        ViewVerificationStatementsBySellers = 1046,

        /// <summary> Просмотр собственных актов сверки </summary>
        ViewVerificationStatementsPersonal = 1047,

        /// <summary> Редактирование актов сверки </summary>
        EditVerificationStatements = 1048,

        /// <summary> Редактирование актов подчиненных пользователей </summary>
        EditVerificationStatementsBySellers = 1049,

        /// <summary> Редактирование собственных актов </summary>
        EditVerificationStatementsPersonal = 1050,

        /// <summary> Мастер подключения электронной отчетности: блок ФНС </summary>
        AvailableFnsEReportMaster = 1051,

        /// <summary> Мастер подключения электронной отчетности: блок ПФР и ФСС </summary>
        AvailablePfrAndFssEReportMaster = 1052,

        /// <summary> Включение интеграции </summary>
        IntegrationAvailable = 1053,

        PrimaryDocumentsSales = 1054,

        /// <summary>
        /// Редактирование документов в покупках
        /// </summary>
        PrimaryDocumentsBuying = 1055,

        /// <summary>
        /// Отображение документов в покупках
        /// </summary>
        ViewPrimaryDocumentsBuying = 1056,

        AccessPrimaryDocumentsSales = 1057,

        AccessPrimaryDocumentsBuying = 1058,

        /// <summary> Разрешить доступ к импорту из 1С (из биллинга) </summary>
        AccessToImportFrom1C = 1060,

        /// <summary> Включение/отключение режима работодателя </summary>
        EditEmployerMode = 1061,

        /// <summary> Консультации экспертов </summary>
        ConsultantAvailable = 1070,

        /// <summary> Более 2000 нетиповых форм документов </summary>
        FormsAndNpdsAvailable = 1071,

        /// <summary> Неограниченное количество вопросов в консультациях </summary>
        NoLimitedConsultations = 1081,

        /// <summary> Мастер бух баланса БИЗ </summary>
        AccessToBalanceBiz = 1082,

        #region ЭДО (1230-1270)

        /// <summary> Отображение раздела ЭДО </summary>
        AccessToEdmSection = 1230,
        
        /// <summary> Отображение раздела ЭДО </summary>
        CanSendEdmDocuments = 1231,
        
        #endregion
        
        #region Настройка шапки (1500 - 1599)
        /// <summary> Показывать кнопку чата в шапке </summary>
        HeaderShowChatButton = 1508,

        /// <summary> Отображение чата WL </summary>
        HeaderShowWlExternalChat = 1509,

        /// <summary> Отобажение чата БЮРО </summary>
        HeaderShowBuroExternalChat = 1511,

        #endregion

        #region Consultations (1901-1909)

        ConsultationsBizLevel = 1901,

        ConsultationsProLevel = 1902,

        ConsultationsUsnLevel = 1903,

        ConsultationsOutsourceLevel = 1904,

        ConsultationsFinguruOutsourceLevel = 1905,

        ConsultationsKnopkaOutsourceLevel = 1906,

        ConsultationsAccLevel = 1908,

        ConsultationsOsnoLevel = 1909,

        ConsultationsManagePeriods = 1910,

        /// <summary> Доступ к распределению консультаций в бизе </summary>
        AccessConsultationsBizManaging = 2038,
        /// <summary> Доступ к ответу на консультации в бизе </summary>
        AccessConsultationsBizAnswering = 2036,
        /// <summary> Доступ к одобрению ответов на консультации в бизе </summary>
        AccessConsultationsBizApproving = 2040,

        /// <summary> Доступ к распределению консультаций в про </summary>
        AccessConsultationsProManaging = 2039,
        /// <summary> Доступ к ответу на консультации в про </summary>
        AccessConsultationsProAnswering = 2037,
        /// <summary> Доступ к одобрению ответов на консультации в про </summary>
        AccessConsultationsProApproving = 2041,

        /// <summary> Доступ к распределению консультаций в Outsource </summary>
        AccessConsultationsOutsourceManaging = 2102,
        /// <summary> Доступ к ответу на консультации в Outsource </summary>
        AccessConsultationsOutsourceAnswering = 2101,
        /// <summary> Доступ к одобрению ответов на консультации в Outsource </summary>
        AccessConsultationsOutsourceApproving = 2103,

        /// <summary> Доступ к редактированию FAQ консультаций </summary>
        AccessConsultationsFaqEditing = 2042,
        /// <summary> Доступ к редактированию информации о консультантах </summary>
        AccessConsultantInfoEditing = 2043,
        /// <summary> Управление тегами </summary>
        AccessConsultationsTagsAdministration = 2044,
        #endregion Consultations

        ViewPostings = 1907,

        /// <summary>
        /// Доступ к звонку в МоёДело по телефону
        /// </summary>
        AccessToPhoneCall = 1912,

        /// <summary>
        /// Доступ к звонку в МоёДело через 'Онлайн звонок'
        /// </summary>
        AccessToWebCall = 1913,

        /// <summary> Просмотр бухгалтерских проводок </summary>
        ViewAccountingPostings = 1999,

        #endregion Клиентская часть

        #region Партнерская часть (от 2001 до 3000)

        /// <summary> Просмотр общей статистики </summary>
        AccessStatistics = 2001,

        /// <summary> Доступ к обзвону / лидам </summary>
        AccessLeads = 2002,

        /// <summary> Просмотр биллинга </summary>
        ViewBilling = 2003,

        /// <summary> Управление биллингом </summary>
        EditBilling = 2004,

        /*
        /// <summary> Доступ к консультациям </summary>
        [Obsolete("Право доступа для старых консультаций. Нынче не используется. См. права 2036-2044")]
        AccessConsultations = 2005,
        */

        /// <summary> Управление партнерами </summary>
        ManagePartners = 2006,

        /// <summary> Управление онлайн-партнерами </summary>
        ManageOnlinePartners = 2007,

        /// <summary> Управление продавцами </summary>
        ManageSalers = 2008,

        /// <summary> Управление администраторами </summary>
        ManageAdministrators = 2009,

        /// <summary> Доступ к продажам </summary>
        AccessSalerLogin = 2010,

        /// <summary> Администратор отчётности </summary>
        UserReportsAdministrator = 2011,

        /// <summary> Запрос ночной автовыписки по банковским интеграциям </summary>
        AccessToIntegrationNightRequests = 2012,

        /// <summary> Пользователь отчётности </summary>
        UserReportsUser = 2013,

        /// <summary> Просмотр купонов </summary>
        ViewCoupones = 2014,

        /// <summary> Редактирование купонов </summary>
        EditCoupones = 2015,

        /// <summary> Управление бизнес версиями </summary>
        ManageBusinessVersion = 2016,

        /// <summary> Управление активацией и удалением пользователей </summary>
        ManageUserAccess = 2017,

        /// <summary> Просмотр поступления денежных средств </summary>
        AccessMoneyProfit = 2018,

        /// <summary> Управление новостями и промо-акциями </summary>
        AccessNewsAndPromo = 2020,

        /// <summary> Управление документами Астрала </summary>
        ManageAstralSinatureDocuments = 2021,

        /// <summary> Логин в партнерку в качестве администратора </summary>
        AccessAdministratorLogin = 2022,

        /// <summary> Логин в партнерку в качестве партнера </summary>
        AccessPartnerLogin = 2023,

        /// <summary> Логин в партнерку в качестве сотрудника партнера </summary>
        AccessPartnerWorkerLogin = 2024,

        /// <summary> Логин в партнерку для онлайн-рефералов </summary>
        AccessReferalLogin = 2025,

        /// <summary> Доступ к продажам для наших продавцов </summary>
        AccessSales = 2026,

        /// <summary> Доступ к перезакреплению лидов </summary>
        AccessManageLead = 2027,

        /*
        /// <summary> Доступ к сводному отчету </summary>
        AccessSummaryReport = 2028,
        */
        
        /// <summary> Доступ к переводу пользователей </summary>
        AccessTransferUsers = 2029,

        /// <summary> Доступ к переводу пользователей </summary>
        AccessWebinarRead = 2030,

        /// <summary> Доступ к переводу пользователей </summary>
        AccessWebinarEdit = 2031,

        /// <summary> Доступ к переводу пользователей </summary>
        AccessWebinarStatistic = 2032,

        /// <summary> Доступ к переводу пользователей </summary>
        AccessEgripEgrul = 2033,

        /// <summary> Прием документов астрала по доверенности </summary>
        AccessReceptionDocByWarrant = 2034,

        /// <summary> Подтверждение документов астрала по доверенности </summary>
        AccessCumfirmByWarrant = 2035,

        /// <summary> Изменение пароля </summary>
        AccessClientChangePassword = 2045,

        /// <summary> Создание временного пароля</summary>
        AccessClientTemporaryPassword = 2046,

        /// <summary> Доступ к разделу "Тестирование" в партнерке </summary>
        AccessTesting = 2047,

        /// <summary>Бухгалтерия - скачивание закрывающих документов" </summary>
        AccountingDownloadDocuments = 2051,

        /// <summary> Бухгалтерия - Акты и закрывающие документы в 1С</summary>
        AccountingActAnd1CDocuments = 2052,

        /// <summary> Доступ к разделу редактирования событий Онлайн-ТВ </summary>
        OnlineTvEditEvents = 2053,

        /// <summary> Изменение логина, состоящего из цифр </summary>
        ChangeClientDigitalLogin = 2054,

        /// <summary> Изменение любого логина </summary>
        ChangeAnyClientLogin = 2055,

        /// <summary> Изменение данных клиента </summary>
        ChangeClientData = 2056,

        /// <summary> Найти клиента клиента </summary>
        FindClient = 2057,

        /// <summary> Бухгалтерия - глав бух </summary>
        AccountantGeneral = 2058,

        /// <summary> Добавлять оплату для всех фирм в outsource аккаунтах</summary>
        ManageOutsourceBilling = 2059,

        /// <summary> Право вносить изменения в контент промо-сайта </summary>
        EditPromoContent = 2060,

        /// <summary> Право утверждать внесенные изменения в контент промо-сайта </summary>
        ApproveEditedPromoContent = 2061,

        /// <summary> Право на создание цифровых логинов для про </summary>
        GenerateDigitalLoginsForPro = 2062,

        /// <summary> Генерировать отчеты по программе привлечения в ПРО с помощью /Presentations/ </summary>
        GeneratePresentationsReport = 2063,

        /// <summary> Право вносить расширенные изменения в контент промо-сайта </summary>
        AdvancedEditPromoContent = 2064,

        /// <summary> Доступ ко вкладке "Обратная связь" </summary>
        ViewCallSituations = 2065,

        /// <summary> Главная статистика по клиентам </summary>
        StatisticsCommonClient = 2066,

        /// <summary> Статистика поступления денежных средств </summary>
        StatisticsMoneyIncoming = 2067,

        /// <summary> Статистика неудавшихся платежей </summary>
        StatisticsUnsuccessfulPayments = 2068,

        /// <summary> Статистика продажи </summary>
        StatisticsSells = 2069,

        /// <summary> Статистика по сопровождению клиентов </summary>
        StatisticsEscort = 2070,

        /// <summary> Статистика по продлениям </summary>
        StatisticsProlongation = 2071,

        /// <summary> Статистика по лидам (Лиды, Список лидов, Конвертация лидов, Источники лидов) </summary>
        StatisticsLeads = 2072,

        /// <summary> Управление триальными картами </summary>
        TrialCardsManaging = 2073,

        /// <summary> Доступ к странице регистрации и активации клиента ПРО </summary>
        AccountantRegistrationPage = 2074,

        /// <summary> Редактирование справочников </summary>
        CatalogsManagement = 2075,

        /// <summary> Заявки на открытие счёта в ПСБ банке </summary>
        PsbBankRequests = 2076,

        /// <summary> Заявки на открытие счёта в альфа банке </summary>
        AlfaBankRequests = 2077,

        /// <summary> Отчёты по запросившим звонок с презентацией с промо-сайта </summary>
        PromoPleaseCallMeRequests = 2078,

        /// <summary> Выгрузки для рассылок </summary>
        EmailSendingBaseGenerating = 2079,

        /// <summary> Человеческое редактирование биллинга </summary>
        AdvancedBillingEditing = 2080,

        /*
        /// <summary> Статистика по переподписке </summary>
        ProlongationScoringStat = 2081,
        */
        
        /// <summary> Редактирование аутсорсеров в Биллинге 2.0 </summary>
        AdvancedBillingOutsource = 2082,

        /// <summary> Заявки на открытие счёта в СДМ банке </summary>
        SdmBankRequests = 2083,

        /// <summary> Просмотр и установка прав пользователей </summary>
        ViewAndSetUserFirmRules = 2084,

        /// <summary> Управление документами Астрала для банка "Открытие"</summary>
        ManageAstralSinatureDocumentsForOpenBank = 2085,

        /// <summary> Заявки на открытие счёта в банке "Открытие" </summary>
        OpenBankRequests = 2086,

        /// <summary> Доступ к вкладке "Регистраторы" </summary>
        RegistratorsView = 2087,

        /// <summary> Доступ к консультационному чату с про пользователями </summary>
        ConsultationsProChat = 2088,

        /// <summary> Доступ к отчету по активности </summary>
        ReportUserActivity = 2089,

        /// <summary> Доступ в BackOffice </summary>
        BackOffice = 2090,

        OnlineTvRubricManagement = 2091,

        /// <summary> Доступ к редактированию списка регистраторов </summary>
        RegistratorsEdit = 2092,

        /// <summary> Доступ к странице подтверждения лидов в BackOffice </summary>
        AccessToLeadStatusChangingAtBackOffice = 2093,

        /// <summary> Доступ к странице подтверждения заявок на региональное представительство в BackOffice </summary>
        AccessToRequestsForVipAtBackOffice = 2094,

        /// <summary> Доступ к странице подтверждения заявок на вывод средств в BackOffice </summary>
        AccessToRequestsForWithdrawalAtBackOffice = 2095,

        /// <summary> Заявки в АнкорБанк </summary>
        AnkorBankRequests = 2096,

        /// <summary> Доступ к странице пополнение личного счета в BackOffice </summary>
        AccessToAccountReplenishmentAtBackOffice = 2097,

        /// <summary> Доступ к странице сводной статистики в BackOffice </summary>
        AccessToSummaryStatisticsAtBackOffice = 2098,

        /// <summary> Статистика по каналам клиентов. </summary>
        ReportChannels = 2099,

        /// <summary> Проверка клиента для банков </summary>
        CheckUserForBank = 2100,

        /// <summary> Доступ к отчету по активности Biz (trial) </summary>
        ReportUserActivityBizTrialCard = 2122,

        /// <summary> Доступ к подвкладке "Продажи и клиентская база" </summary>
        AccessToSalesAndUsersReport = 2104,

        /// <summary> Доступ к флагу "Не учитывать в статистике" в биллинге </summary>
        AccessToChangeStatisticFlagInBilling = 2105,

        /// <summary> Заявки в Траст банк </summary>
        TrustBankRequests = 2106,

        /// <summary> Заявки в Локо банк </summary>
        LokoBankRequests = 2107,

        /// <summary> Заявки в Райффайзен банк </summary>
        RaiffeisenBankRequests = 2108,

        /// <summary> Заявки в CБ банк </summary>
        SbBankRequests = 2109,

        /// <summary> Прочие заявки </summary>
        CustomBankRequests = 2110,

        /// <summary> Заявки в Модуль-Банк </summary>
        ModulBankRequests = 2120,

        /// <summary> Доступ к распределению консультаций в Usn </summary>
        AccessConsultationsUsnManaging = 2111,

        /// <summary> Доступ к одобрению ответов на консультации в Usn </summary>
        AccessConsultationsUsnApproving = 2112,

        /// <summary> Доступ к ответу на консультации в Usn </summary>
        AccessConsultationsUsnAnswering = 2113,

        /// <summary> Доступ к странице редактора ставок транспортного налога </summary>
        AccessToTransportTaxRate = 2114,

        /// <summary> Доступ к отчету Продажи и клиентская база 2 </summary>
        AccessToSalesAndUsersReport2 = 2118,

        /// <summary> Доступ к ответу на консультации в FinguruOutsource </summary>
        AccessConsultationsFinguruOutsourceAnswering = 2500,

        /// <summary> Доступ к распределению консультаций в FinguruOutsource </summary>
        AccessConsultationsFinguruOutsourceManaging = 2501,

        /// <summary> Доступ к одобрению ответов на консультации в FinguruOutsource </summary>
        AccessConsultationsFinguruOutsourceApproving = 2502,

        /// <summary> Доступ к ответу на консультации в KnopkaOutsource </summary>
        AccessConsultationsKnopkaOutsourceAnswering = 2503,

        /// <summary> Доступ к распределению консультаций в KnopkaOutsource </summary>
        AccessConsultationsKnopkaOutsourceManaging = 2504,

        /// <summary> Доступ к одобрению ответов на консультации в KnopkaOutsource </summary>
        AccessConsultationsKnopkaOutsourceApproving = 2505,

        /// <summary> Доступ к настройкам пула тестовых данных </summary>
        AccessTestDataPool = 2115,

        /// <summary> Просмотр акций </summary>
        MrkActionsView = 2116,

        /// <summary> Редактирование акций </summary>
        MrkActionsEdit = 2117,

        /// <summary> Доступ к странице профессиональных аутсорсеров </summary>
        AccessToProfOutsource = 2119,

        /// <summary> Доступ к странице Интеграция </summary>
        AccessToIntegration = 2121,

        /// <summary> Доступ в партнёрку в качестве сотрудника банка-партнёра </summary>
        AccessBankWorkerLogin = 2123,

        /// <summary> Доступ в партнёрку в качестве сотрудника-администратора банка-партнёра </summary>
        AccessBankWorkerAdmin = 2124,

        /// <summary> Право на установку/снятие флага «не выгружать документы в 1С» на странице "Биллинг" в партнерке </summary>
        EditPaymentIsDownloadFlag = 2125,

        /// <summary> Выставление счетов</summary>
        CreateBill = 2126,

        /// <summary> Просмотр импортированных выписок</summary>
        AccessToViewImportedPayments = 2127,

        /// <summary> Разнесение платежей</summary>
        PaymentImportNotMapped = 2128,

        /// <summary> Редактирование операторов</summary>
        EditOperators = 2129,

        /// <summary> Управление операторами</summary>
        ManageOperators = 2130,

        /// <summary> Просмотр заявок на открытие счёта для любого банка </summary>
        AllBankRequests = 2131,

        /// <summary> Биллинг: Вернуть пользователя на шаги мастера ИП/ООО </summary>
        RevertToMasterIpOoo = 2132,

        /// <summary> Вход в партнёрский кабинет в качестве партнёра-администратора </summary>
        AccessPartnerAdministratorLogin = 2133,

        /// <summary> Доступ к странице подтверждения лидов (каналы лидов) в BackOffice </summary>
        AccessToLeadChannelStatusChangingAtBackOffice = 2134,

        /// <summary> Доступ к выставлению счетов в партнёрском кабинете </summary>
        CreateBillForPartner = 2135,

        /// <summary> Доступ к календарю  </summary>
        AccessCalendar = 2136,

        /// <summary> Главный сотрудник Альфа-банка </summary>
        AlfaBankRegionalPartnerAdmin = 2137,

        /// <summary> Доступ к странице регистрации в Партнерке </summary>
        AccessToPartnerRegistrationPage = 2138,

        /// <summary> Доступ к странице настроек источников лидов в Партнерке </summary>
        AccessToLeadSourceManagementPage = 2139,

        /// <summary> Маркетинг. Редактирование сегментов контекста </summary>
        AccessToMarketingContextSegmentEdit = 2140,

        /// <summary> Доступ к странице регистрации заявок на РКО </summary>
        AccessToBanksRequestCreate = 2140,

        /// <summary> Доступ к отчету с матрицей конверсии </summary>
        AccessToReportConversionRate = 2141,

        /// <summary> Доступ к дополнительным возможностям редактирования билинга </summary>
        AccessToСhangeBillingPaymentSuccessStateInHistory = 2142,

        /// <summary> Доступ к управлению сертификатами Яндекс API </summary>
        AccessToYandexApiSertificatesManagement = 2143,

        /// <summary> Доступ к метрикам и событиям пользователей </summary>
        AccessToUserTelemetry = 2144,

        /// <summary> Доступ к редактирования UTM-меток в биллинге </summary>
        AccessToChangeUtmFields = 2145,

        /// <summary> Доступ к переносу SalesForce задач </summary>
        AccessToManageSFTasks = 2146,

        /// <summary> Доступ к запуску автоматического выставления счетов  </summary>
        AccessToAutoCreateBill = 2147,

        /// <summary> Запрет на генерацию временного пароля для пользователя, у которого есть это право</summary>
        ProhibitionToGenerateTemporaryPasswordForUser = 2148,

        /// <summary> Право, которое дает возможность удалять сообщения в консультациях в партнерке</summary>
        AccessToDeleteConsultationMessage = 2149,

        /// <summary> Доступ к вкладке создания оповещений пользователям</summary>
        AccessToNotifications = 2150,

        /// <summary> Расширенный доступ к вкладке создания оповещений пользователям</summary>
        AdvancedAccessToNotifications = 2151,

        /// <summary> Доступ к редактированию тарифа в платеже в Биллинге 2.0 </summary>
        AccessToChangePaymentTariff = 2152,

        /// <summary> Доступ к редактированию платежа в Биллинге 2.0 </summary>
        AccessToChangePayment = 2153,

        /// <summary> Доступ к просмотру автоматического выставления счетов Аутсорсинга </summary>
        AccessToViewAutoCreteBillForOutsource = 2154,

        /// <summary> Доступ к запуску автоматического выставления счетов Аутсорсинга </summary>
        AccessToStartAutoCreteBillForOutsource = 2155,

        /// <summary> Доступ к просмотру автоматического выставления счетов БИЗа </summary>
        AccessToViewAutoCreteBillForBiz = 2156,

        /// <summary> Доступ к запуску автоматического выставления счетов БИЗа </summary>
        AccessToStartAutoCreteBillForBiz = 2157,

        /// <summary> Доступ к вкладке загрузка баз лидов </summary>
        AccessLeadImport = 2158,

        /// <summary> Доступ к просмотру автоматического выставления счетов пользователям Убер </summary>
        AccessToViewAutoCreteBillForUber = 2159,

        /// <summary> Доступ к запуску автоматического выставления счетов пользователям Убер </summary>
        AccessToStartAutoCreteBillForUber = 2160,

        /// <summary> Доступ к просмотру автоматического выставления счетов пользователям Гольфстрим </summary>
        AccessToViewAutoCreteBillForGolfstream = 2161,

        /// <summary> Доступ к запуску автоматического выставления счетов пользователям Гольфстрим </summary>
        AccessToStartAutoCreteBillForGolfstream = 2162,

        /// <summary> Доступ к просмотру логов </summary>
        AccessToViewLogs = 2163,

        /// <summary> Доступ к управлению ошибками Kayako </summary>
        AccessToManageKayakoErrors = 2164,

        ResendBill = 2165,

        /// <summary> Вкладка смена статусов в CRM </summary>
        CrmChangeStatus = 2166,

        /// <summary> Вкладка отправка в автообзвон </summary>
        CrmSendToAutoDial = 2167,

        /// <summary> Отчёт план-факт по продажам </summary>
        PlanFactReport = 2168,

        /// <summary> Загружать настройки для план факта </summary>
        PlanFactReportUploadSettings = 2169,

        /// <summary> Партнёрка. Доступ к вкладке Бюро </summary>
        AccessBuroPage = 2170,

        /// <summary> Доступ к просмотру номеров телефонов в биллнге </summary>
        AccessToViewPhones = 2171,

        /// <summary> Доступ к просмотру номеров телефонов в отчетах </summary>
        AccessToViewPhonesInReport = 2172,

        /// <summary> Доступ к импорту выписок</summary>
        AccessToImportPayments = 2173,

        /// <summary> Доступ к просмотру расходных операций в импортированных выписках </summary>
        AccessToViewOutgoingInImportedPayments = 2174,

        /// <summary> Доступ к просмотру остатков в импортированных выписках </summary>
        AccessToViewEndBalanceInImportedPayments = 2175,

        /// <summary> Вкладка отправка на переподписку </summary>
        CrmSendToOverSubscription = 2176,

        /// <summary> Доступ к сверке операций в биллинге с выписками электронных платежных систем </summary>
        AccessToCheckBillingWithAcquiring = 2177,

        /// <summary> Доступ к созданию платежей набором </summary>
        AccessToCreatePaymentsPack = 2178,

        /// <summary> Доступ к управлению прайс-листами </summary>
        AccessToManagePriceLists = 2179,

        /// <summary> Вкладка отправка контрагентов в автообзвон</summary>
        CrmSendAccountsToAsterisk = 2180,

        /// <summary> Отчёт о сроке подписки </summary>
        SubscriptionPeriodReport = 2181,

        /// <summary> Доступ к сверке операций в биллинге с выписками из банка </summary>
        AccessToCheckBillingWithBanks = 2182,

        /// <summary> Доступ к управлению привязкой аккаунта банковских интеграций </summary>
        AccessToManageIntegratedUsers = 2183,

        /// <summary> Доступ к скрытию нераспознанных платежей импортированных выписок</summary>
        AccessToHideNotMappedImportedPayments = 2184,

        /// <summary> Доступ к сверке операций биллинге с самим биллингом </summary>
        AccessToCheckBillingWithBilling = 2185,

        /// <summary> Доступ к распознаванию платежек в нераспознанных по идентификатору платежа в биллинге </summary>
        PaymentImportMapByPaymentId = 2186,

        /// <summary> Право на обновление кодов фондов в заявках на вкладке "Отчётность Астрал" </summary>
        UpdateFundCodes = 2187,

        /// <summary> Доступ к просмотру отчетов в кабинете регионального партнера </summary>
        CanViewPartnerReports = 2188,

        /// <summary> Доступ к вкладке "Подготовка данных для обучения"</summary>
        AccessTrainingDataPrepare = 2189,        
        
        /// <summary> Биллинг. Смена продавца платежа </summary>
        AccessToChangePaymentSaler = 2190,

        /// <summary> Биллинг. Доступ к смене email адреса для автовыставления счёта </summary>
        AccessChangeAutoCreateBillEmail = 2191,

        /// <summary> Биллинг. Смена продавца платежа пачкой </summary>
        AccessToChangePaymentPackSaler = 2192,

        /// <summary> Просмотр списка своих счетов в новом биллинге </summary>
        BackofficeBillingViewMyBillsPage = 2230,

        /// <summary> Выставление полных и частичных возвратов </summary>
        EditRefunds = 2240,

        /// <summary> Просмотр и редактирование акций на странице "Клуб предпринимателей" </summary>
        AccessBusinessmanClub = 2513,

        /// <summary> Доступ к уведомлениям </summary>
        AccessToSystemNotification = 2514,

        /// <summary> Доступ к разделу Программа "Приведи друга" </summary>
        AccessToFriendInvite = 2515,

        /// <summary> Списание средств по программе "Приведи друга" </summary>
        AccessToWriteOffBonusesFriendInvite = 2516,

        /// <summary> Биллинг. Редактирование связанных с платежом оплат </summary>
        AccessToEditPaymentTransactions = 2517,

        /// <summary> Партнерский кабинет Доступ к контактам менеджеров в шапке </summary>
        PartnerAccessToSupportHeader = 2518,

        /// <summary> Партнерский кабинет Доступ к странице Заявка </summary>
        PartnerAccessToRequestPage = 2519,

        /// <summary> Партнерский кабинет Доступ к странице Реферальная ссылка </summary>
        PartnerAccessToReferralLinkPage = 2520,

        /// <summary> Партнерский кабинет Доступ к странице Клиенты </summary>
        PartnerAccessToClientsPage = 2521,

        /// <summary> Партнерский кабинет Доступ к странице Сотрудники </summary>
        PartnerAccessToEmployeesPage = 2522,

        /// <summary> Партнерский кабинет Доступ к странице Статистика </summary>
        PartnerAccessToStatisticsPage = 2523,

        /// <summary>
        /// Партнерский кабинет Доступ к развилке "существующий/новый пользователь" на странице выставления счета
        /// </summary>
        PartnerAccessToNewExistingUserBillFields = 2524,

        /// <summary> Партнерский кабинет Доступ к полю "Вид операции" на странице выставления счета </summary>
        PartnerAccessToOperationTypeBillField = 2525,

        /// <summary> Партнерский кабинет Доступ к полю "Плательщик" на странице выставления счета </summary>
        PartnerAccessToPayerBillField = 2526,

        /// <summary> Партнерский кабинет Доступ к полю "Промокод" на странице выставления счета </summary>
        PartnerAccessToPromocodeBillField = 2527,

        /// <summary> Партнерский кабинет Доступ к полю "Нормативная сумма платежа" на странице выставления счета </summary>
        PartnerAccessToDefaultSumBillField = 2528,

        /// <summary> Партнерский кабинет Доступ к полю "Актуальная сумма платежа" на странице выставления счета </summary>
        PartnerAccessToSumBillField = 2529,

        /// <summary> Партнерский кабинет Доступ к полю "Примечание" на странице выставления счета </summary>
        PartnerAccessToNoteBillField = 2530,

        /// <summary> Партнерский кабинет Доступ к полю "Не отправлять счет клиенту" на странице выставления счета </summary>
        PartnerAccessToNotSendToUserBillFiled = 2531,

        /// <summary> Партнерский кабинет Доступ к странице Цифровые логины </summary>
        PartnerAccessToDigitalLoginsPage = 2532,

        /// <summary> Партнерский кабинет Доступ к Отчету по активности ИБ </summary>
        PartnerAccessToActivityBizReport = 2533,

        /// <summary> Партнерский кабинет Доступ к Отчету по активности БЮРО </summary>
        PartnerAccessToActivityBuroReport = 2534,

        /// <summary> Партнерский кабинет Доступ к Отчету о переподписке ИБ </summary>
        PartnerAccessToRenewSubscriptionsBizReport = 2535,

        /// <summary> Партнерский кабинет Доступ к Отчету о переподписке БЮРО </summary>
        PartnerAccessToRenewSubscriptionsBuroReport = 2536,

        /// <summary> Партнерский кабинет Доступ к Отчету о переподписке АУТСОРС </summary>
        PartnerAccessToRenewSubscriptionsOutsourceReport = 2537,

        /// <summary> Партнерский кабинет Доступ к Отчет о выставленных счетах </summary>
        PartnerAccessToCreatedBillsReport = 2538,

        /// <summary> Партнерский кабинет Отображение поля с выбором тарифа на форме регистрации </summary>
        PartnerAccessToChangeTariffFieldOnTradeTrial = 2539,

        /// <summary> Доступ к просмотру Автовыставление счетов 2.0 </summary>
        AccessToViewAutoBillingIB = 2540,

        /// <summary> Доступ к изменению настроек системы Автовыставление счетов 2.0 </summary>
        AccessToEditSystemAutoBillingIB = 2541,

        /// <summary> Доступ к изменению Запуска системы Автовыставление счетов 2.0 </summary>
        AccessToEditInitiateAutoBillingIB = 2542,

        /// <summary> Доступ к автоматизации биллинга "Интеграция с банком для Финансиста" </summary>
        AccessToFinControlBillingAutomation = 2543,

        /// <summary> Доступ к странице выбора операторов для анализа записей звонков </summary>
        AccessToCallAnalytics = 2544,

        /// <summary> Доступ к просмотру Автовыставление счетов 2.0 </summary>
        AccessToViewAutoBillingOUT = 2545,

        /// <summary> Доступ к изменению настроек системы Автовыставление счетов 2.0 </summary>
        AccessToEditSystemAutoBillingOUT = 2546,

        /// <summary> Доступ к изменению Запуска системы Автовыставление счетов 2.0 </summary>
        AccessToEditInitiateAutoBillingOUT = 2547,
        
        /// <summary> Доступ к редактированию примечаний у платежей </summary>
        AccessToEditNotesForPayments = 2548,

        #endregion Партнерская часть

        #region Бухгалтерская часть

        /// <summary> Логин в интерфейс бухгалтера </summary>
        AccessAccountantLogin = 3001,

        /// <summary> Прием денег агрегатором от обслуживаемого клиента </summary>
        AccessReceiptMoneyForAggregator = 3002,

        /// <summary> Доступ к складу </summary>
        AccessToStock = 3100,

        /// <summary> Доступ к имуществу </summary>
        AccessToEstate = 3101,

        AccessToAccountingPolicies = 3102,

        /// <summary> Доступ к просмотру банка </summary>
        AccessToViewAccountingBank = 3103,

        /// <summary> Доступ к редактированию в банке </summary>
        AccessToEditAccountingBank = 3104,

        /// <summary> Доступ к просмотру бухгалтерской справки </summary>
        AccessToViewAccountingStatements = 3501,

        /// <summary> Доступ на редактирование бухгалтерской справки </summary>
        AccessToEditAccountingStatements = 3502,

        /// <summary> Доступ к аналитике </summary>
        AccessToAnalytics = 3601,
        
        /// <summary> Услуги бухгалтера на ИБ </summary>
        AccountantServicesOnIB = 3602,

        #endregion Бухгалтерская часть

        #region Интеграции (от 4950 до 4999)
        SberbankWLOperator = 4950,

        WlMtsTariffs = 4953,

        /// <summary>
        /// Заявки на платеж и маршруты согласования: чтение
        /// </summary>
        PaymentRequestsModuleRead = 4960,

        /// <summary>
        /// Заявки на платеж: редактирование
        /// </summary>
        PaymentRequestsEdit = 4961,

        /// <summary>
        /// Маршруты согласования заявок на платеж: редактирование
        /// </summary>
        PaymentRequestWorkflowsEdit = 4962,

        SberbankWLSubscriptionAny = 4987,

        IntegrationWithUralsibSso = 4988,

        IntegrationWithAlfabankSso = 4989,

        IntegrationWithRobokassa = 4996,

        IntegrationWithSape = 4997,

        IntegrationWithSkb = 4999,
        #endregion Интеграции

        #region СПС

        /// <summary> Логин в спс </summary>
        SpsLogin = 5000,

        /// <summary> Обратиться к консультанту </summary>
        ConsultantRequest = 5001,

        /// <summary> Просмотр Вопросов-Ответов </summary>
        ViewAnswerQuestion = 5002,

        /// <summary> Заполнение бланков </summary>
        FillBlanks = 5003,

        /// <summary> Полный доступ к просмотру Вопросов-Ответов </summary>
        QuestionsFullAccess = 5004, // Устаревший

        /// <summary> Доступ к странице Вопросов-Ответов </summary>
        SpsAccessToSectionQa = 5005,

        /// <summary> Доступ к странице Правовой базы </summary>
        SpsAccessToSectionNpd = 5006,

        /// <summary> Доступ к странице Бланков </summary>
        SpsAccessToSectionForm = 5007,

        /// <summary> Доступ к странице Обзоров </summary>
        SpsAccessToSectionInfo = 5008,

        /// <summary> Доступ к странице Практик консультаций </summary>
        SpsAccessToSectionPractice = 5009,

        #endregion СПС

        #region Зарплата

        /// <summary> Логин в зарплату </summary>
        PayrollAvailable = 6000,

        ImportFrom1C = 6001,
        
        MaxFiveWorkers = 6002,

        #endregion Зарплата

        #region Тарифные права

        UsnAccountantTariff = 7000,

        IpWithoutWorkersTariff = 7001,

        OooWithoutWorkersTariff = 7002,

        WithWorkersTariff = 7003,

        IpRegistrationTariff = 7004,

        OooRegistrationTariff = 7005,

        AccountantConsultatntTariff = 7006,

        AccountantChamberTariff = 7007,

        SalaryAndPersonalTariff = 7014,

        AccountantConsultantSmallBusinessTariff = 7015,

        AccountantChamberSmallBusinessTariff = 7016,

        SalaryAndPersonalSmallBusinessTariff = 7017,

        DigitalSignTariff = 7018,

        AccountantConsultantMultiuserTariff = 7019,

        OutsourceKnopkaTariff = 7020,

        OutsourceFinguruTariff = 7021,

        ProfOutsourceTariff = 7022,

        OutsourceTariff = 7023,

        /// <summary>
        /// Скрывается напоминание об оплате при наличии права
        /// и просто подтверждает принадлежность к тарифу
        /// </summary>
        PsbBankTariff = 7024,

        /// <summary>
        /// Подтверждает принадлежность к тарифу Интеза
        /// </summary>
        IntesaBankTariff = 7025,

        /// <summary>
        /// Подтверждает принадлежность к тарифу РНКБ
        /// </summary>
        RnkbTariff = 7026,
        
        /// <summary>
        /// Подтверждает принадлежность к группе тарифов "Проф. бухгалтер"
        /// </summary>
        ProfessionalAccountantTariff = 7027,

        /// <summary>
        /// Подтверждает принадлежность к тарифу ПростоБанк
        /// </summary>
        ProstoBankTariff = 7028,

        /// <summary>
        /// Подтверждает принадлежность к тарифу АкбарсБанк
        /// </summary>
        AkbarsBankTariff = 7029,

        /// <summary>
        /// Подтверждает принадлежность к тарифу АльфаБанк
        /// </summary>
        AlphaBankTariff = 7030,

        /// <summary>
        /// Принадлежность к БИЗу
        /// </summary>
        BizPlatform = 7070,

        SberbankTariff = 7075,

        CaseLookTariff = 7076,

        SberbankTariffWithRestrictions = 7077,

        /// <summary>
        /// мастера регистрации Е-регистратора
        /// </summary>
        MasterOfRegistrationTariff = 7078,

        SberbankWLTariff = 7079,

        /// <summary> Сбербанк. Квалифицированная электронная подпись </summary>
        SberbankQualifiedElectronicSignatureTariff = 7080,

        /// <summary>Признакт тарифов СКБ Банка</summary>
        SkbBankWlTariff = 7081,
        
        /// <summary> Биллинг 2.0. Возможность изменения суммы тарифа при выставлении платежа </summary>
        AccessToChangeTariffSum = 7082,

        /// <summary> Признак тарифа "Отчётность" СКБ </summary>
        SkbBankWlReportTariff = 7083,
        
        /// <summary> признак того, что это тариф с кассой </summary>
        CashTariff = 7084,

        /// <summary> White Label тариф </summary>
        WlTariff = 7085,
        
        /// <summary>Тариф товароучёта </summary>
        ProductAccountingTariff = 7086,

        /// <summary>Признак товароучета. Само право не означает доступ к функциям товароучета. Логика доступа здесь Stock/api/v1/productAccounting </summary>
        TradeManagementOption = 7087,

        /// <summary>
        /// Партнерский кабинет. Возможность формирования акта в 1С на основе платежа по тарифу
        /// </summary>
        ImportedTo1CTariff = 7088,

        /// <summary>Тариф товароучёта без бухгалтерской отчётности </summary>
        ProductAccountingOnlyTariff = 7089,

        /// <summary> Тариф Премиум </summary>
        PremiumTariff = 7090,
        
        /// <summary> Подтверждает принадлежность к тарифу Управленческий учет </summary>
        ManagementAccountingTariff = 7091,

        /// <summary> Подтверждает принадлежность к тарифу Сервис ликвидации ИП </summary>
        IpEliminationTariff = 7092,

        /// <summary>Признак тарифа Трансфер с ЕНВД</summary>
        TransferFromEnvdTariff = 7093,

        /// <summary> Признак доступности мастера закрытия ИП для платных пользователей</summary>
        IpEliminationTariffForPaid = 7094,

        /// <summary> Признак доступности выпуска ЭЦП для Закрытия ИП в новом биллинге</summary>
        ElectronicSignatureForIpElimination = 7095,

        /// <summary>
        /// Признак тарифа ГИС ЖКХ
        /// </summary>
        GisTariff = 7096,

        /// <summary>
        /// Признак тарифа Интеграция с банком для Финансиста
        /// </summary>
        FinControlIntegrationWithBanksTariff = 7097,

        /// <summary>
        /// Признак права пользователя: полный доступ к сервису для пользователей под управлением ПА
        /// </summary>
        FullAccessWithProfOutsource = 7098,

        /// <summary>
        /// Признак тарифа нулевая отчетность
        /// </summary>
        UsnZeroReportTariff = 7099,

        /// <summary>
        /// Признак группы тарифов для частичного аутсорсинга
        /// </summary>
        PartialAccessToOutsource = 7100,  

        /// <summary>
        /// Признак группы тарифов для полного аутсорсинга
        /// </summary>
        FullAccessToOutsource = 7101,  
        
        /// <summary>
        /// Признак группы тарифов для WL Совкомбанка
        /// </summary>
        SovcombankWlTariff = 7102,

        /// <summary>
        /// Признак группы тарифов для WL Wildberries
        /// </summary>
        WbBankWlTariff = 7103,

        /// <summary>
        /// Тариф из системы "Новый биллинг"
        /// Для использования в платежах, созданных из системы "Новый биллинг"
        /// Такие платежи имеют сложный составной характер и неограниченную вариативность
        /// </summary>
        CompoundTariff = 7777,

        #endregion Тарифные права

        OsnoPermission = 8000,

        /// <summary>
        /// Просмотр видеоуроков
        /// </summary>
        VideoLessonsView = 8001,

        /// <summary>
        /// Шапка в2
        /// </summary>
        Header2 = 8002,

        /// <summary> Расширенные настройки информационного обслуживания налогоплательщиков (ИОН) </summary>
        IonExtendedSettings = 8003,

        /// <summary>
        /// Просмотр страниц в разработке
        /// </summary>
        MdStaff = 8888,

        /// <summary>
        /// Право на редирект в превью (биз)
        /// </summary>
        BizPreview = 8889,

        /// <summary>
        /// Показывае бабл по рекламной акции 28.04.2014
        /// </summary>
        BablForAdvertisement = 8995,

        /// <summary>Скрываем рекламу бухгалтерии под ключ, если пользователь уже побывал на странице акции</summary>
        HideAccountingTurnkey = 4555,

        #region Разделы учетки

        /// <summary>
        /// Показывать главную страницу учетки
        /// </summary>
        AccessToAccountingMain = 9000,

        /// <summary>
        /// Просматривать кассу в учетке
        /// </summary>
        AccessToViewAccountingCash = 9001,

        /// <summary>
        /// Редактировать кассу в учетке
        /// </summary>
        AccessToEditAccountingCash = 9002,

        /// <summary>
        /// Просматривать покупки в учетке
        /// </summary>
        AccessToViewAccountingBuy = 9003,

        /// <summary>
        /// Редактировать покупки в учетке
        /// </summary>
        AccessToEditAccountingBuy = 9004,

        /// <summary>
        /// Доступ в раздел консультант бухгалтер
        /// </summary>
        AccessToAccountingPro = 9005,

        /// <summary>
        /// Доступ в раздел отчеты
        /// </summary>
        AccessToAccountingReports = 9006,

        /// <summary>
        /// Доступ в раздел электронная отчетность
        /// </summary>
        AccessToAccountingEReports = 9007,

        /// <summary>
        /// Доступ в раздел выписки
        /// </summary>
        AccessToAccountingEgr = 9008,

        /// <summary>
        /// Доступ в раздел вебинары
        /// </summary>
        AccessToAccountingWebinars = 9009,

        /// <summary>
        /// Показывать инструменты
        /// </summary>
        AccessToTools = 9010,

        /// <summary>
        /// Это кладовщик
        /// </summary>
        AccessAsStorekeeper = 9011,

        /// <summary>
        /// Это менеджер по продажам
        /// </summary>
        AccessAsSaleManager = 9012,

        /// <summary>
        /// Можно редактировать п/п и КО оплаченные клиентом
        /// </summary>
        AccessEditClientPayments = 9013,

        /// <summary>
        /// Можно редактировать п/п и КО оплаченные клиентом
        /// </summary>
        AccessViewClientPayments = 9014,

        /// <summary>
        /// Доступ в склад только на чтение
        /// </summary>
        AccessToStockSaleOnly = 9015,

        /// <summary>
        /// Доступ к исходящим накладным
        /// </summary>
        AccessEditWaybillSalesOnly = 9016,

        /// <summary>
        /// Доступ к входящим накладным
        /// </summary>
        AccessEditWaybillBuyOnly = 9017,

        /// <summary>
        /// Доступ к импорту выписок в банке
        /// </summary>
        AccessToImportDischard = 9018,

        /// <summary>
        /// Доступ к оборотам в банке и кассе
        /// </summary>
        AccessToTurnovers = 9020,

        /// <summary>
        /// Доступ к актам сверки в учетке
        /// </summary>
        AccessToReconciliationStatements = 9021,

        /// <summary>
        /// Доступ к импорту номенклатур в складе
        /// </summary>
        AccessToStockNomenclatureImport = 9022,
        
        /// <summary>
        /// Доступ к валютным счетам
        /// </summary>
        AccessToCurrencySettlementAccount = 9023,
        
        /// <summary>
        /// Доступ к разделу Клуб предпринимателя
        /// </summary>
        AccessToBusinessmanClub = 9024,

        /// <summary>
        /// Доступ к функционалу "Маркетплейсы и комиссионеры"
        /// </summary>
        AccessToMarketplacesAndCommissionAgents = 9025,

        /// <summary>
        /// Доступ во все разделы консультаций
        /// </summary>
        AccessToAllConsultations = 9026,

        /// <summary> Доступ в раздел МЧД (Машиночитаемые доверенности)</summary>
        AccessToM4D = 9027,
        #endregion

        #region Права на разделы в МП

        MobileMain = 10001,

        MobileMoney = 10002,

        #endregion

        #region Агентская программа

        WebmasterRole = 21100,

        AccessToReferalLink = 21110,

        AccessToPartnerReferalLink = 21120,

        TrialAgentRole = 21200,

        AccessToClientBase = 21210,

        AgentRole = 21300,

        ClientManagementInClientBase = 21310,

        BankRole = 21400,

        AccessToBankLead = 21410,

        #endregion

        #region Бюро (от 30000 до 30100)

        OfficeTariffGroup = 30000,

        BillsAndDocumentsTariff = 30001,

        OfficeTariff = 30002,

        OfficeProTariff = 30003,

        AccessToOfficeMain = 30004,

        AccessToCheckKontragent = 30005,

        ArbitrationCourt = 30006,

        /// <summary>
        /// Доступ к Бланкам
        /// </summary>
        AccessToOfficeForms = 30007,

        AccessToOfficeDepartmentalInspection = 30008,

        AccessToOfficeCalculators = 30009,

        AccessAsBillsAndDocumentsManager = 30010,

        /// <summary>
        /// Доступ к справкам
        /// </summary>
        AccessToInfos = 30011,

        OfficeLiteTariff = 30012,

        CheckKontragentFullAccess = 30013,

        AccessToCheckContragentsForTrial = 30014,

        SpsRestrictForTrial = 30015,

        CheckContragentOnly = 30016,

        AccessToContragentRelations = 30017,

        AccessToContragentBankruptcy = 30018,

        /// <summary>Доступ к юридическим консультациям</summary>
        AccessToJuridicalConsultations = 30019,

        AccessToContragentMassCheck = 30020,

        /// <summary>Доступ к выпискам с кэп</summary>
        AccessToSignedExcerpt = 30021,

        AccessToKontragentFilters = 30022,

        AccessToLaunderingScoring = 30023,

        /// <summary>
        /// E-mail нотификация о старте тарифа
        /// </summary>
        BuroTariffStartNotifications = 30027,

        #endregion

        #region Проф. аутсорс (от 40000 до 50000)

        /// <summary>Доступ к кабинету администратора</summary>
        AccessToAdminChamber = 40000,

        /// <summary>Возможность создавать несколько фирм</summary>
        AccessToPossibilityOfCreationMultipleFirms = 40001,

        /// <summary>Доступ к странице компаний в кабинете администратора</summary>
        AccessToAdminChamberCompaniesPage = 40100,

        /// <summary>Доступ к странице пользователей в кабинете администратора</summary>
        AccessToAdminChamberUsersPage = 40200,

        /// <summary>Доступ к странице подключений в кабинете администратора</summary>
        AccessToAdminChamberInvitePage = 40300,

        /// <summary>Возможность выбора компании</summary>
        AccessToCompanyChange = 41000,

        /// <summary>Юридические консультации</summary>
        LegalAdvice = 41100,

        #endregion

        #region Доступ к отчётам (от 50000 до 51000)

        /// <summary>
        /// Просмотр всех отчетов
        /// </summary>
        ViewReports = 50000,

        /// <summary>
        /// Редактирование отчетов
        /// </summary>
        EditReports = 50001,

        /// <summary>
        /// Создание отчетов ФНС
        /// </summary>
        CreateFnsReport = 50002,

        /// <summary>
        /// Создание отчетов ПФР
        /// </summary>
        CreatePfrReport = 50003,

        /// <summary>
        /// Создание отчетов Росстата
        /// </summary>
        CreateRosStatReport = 50004,

        /// <summary>
        /// Создание отчета 4-ФСС
        /// </summary>
        Create4FssReport = 50005,

        /// <summary>
        /// Создание отчета 2-НДФЛ
        /// </summary>
        Create2NdflReport = 50006,

        /// <summary>
        /// Создание отчета Декларация по НДС
        /// </summary>
        CreateNdsDeclarationReport = 50007,

        /// <summary>
        /// Создание отчета Налог на прибыль
        /// </summary>
        CreateTaxOnProfitReport = 50008,

        /// <summary>
        /// Создание отчета Журнал счетов-фактур
        /// </summary>
        CreateJournalOfInvoicesReport = 50009,

        /// <summary>
        /// Создание отчета Книга покупок
        /// </summary>
        CreateBookPurchasesReport = 50010,

        /// <summary>
        /// Создание отчета Книга продаж
        /// </summary>
        CreateBookSalesReport = 50011,

        /// <summary>
        /// Статистика сдачи отчетности
        /// </summary>
        ReportingStatistics = 50012,

        /// <summary>
        /// Управление сторонними календарными событиями
        /// </summary>
        ManageThirdPartyCalendarEvents = 50013,

        /// <summary>
        /// Создание стороннего календарного события
        /// </summary>
        CreateThirdPartyCalendarEvent = 50014,

        /// <summary>
        /// Упрощенный режим в мастере авансов/декларации УСН
        /// </summary>
        AccessSimpleModeForUsnReport = 50015,

        /// <summary>
        /// Упрощенный режим в мастере фиксированных взносов
        /// </summary>
        AccessSimpleModeForPfrFixesReport = 50016,

        /// <summary>
        /// Доступ к ФинРепорт.МД (финансист), право "Ежедневная финансовая отчётность"
        /// </summary>
        AccessToFinReport = 50017,
        
        /// <summary>
        /// Режим нулевой декларации в мастере УСН
        /// </summary>
        AccessZeroModeForUsnReport = 50018,
        
        /// <summary>
        /// Упрощенный режим в мастере бух. отчетности
        /// </summary>
        AccessSimpleModeForAccountingReport = 50019,

        #endregion

        #region Доступ к аккаунт-кабинету (от 70000 до 80000)

        /// <summary>Доступ к аккаунт-кабинету</summary>
        AccountAccessToPages = 70000,

        /// <summary>Доступ к странице компаний в аккаунт-кабинете</summary>
        AccountAccessToCompaniesPage = 71000,

        /// <summary>Возможность создавать несколько фирм</summary>
        AccountAccessToPossibilityOfCreationMultipleFirms = 71001,

        /// <summary>Возможность выбора компании</summary>
        AccountAccessToCompanyChange = 71002,

        /// <summary>Доступ к странице пользователей в аккаунт-кабинете</summary>
        AccountAccessToUsersPage = 72000,

        /// <summary>Доступ к редактированию пользователей в аккаунт-кабинете</summary>
        AccountAccessToEditUsers = 72001,

        /// <summary>Доступ к странице подключений в аккаунт-кабинете</summary>
        AccountAccessToInvitesPage = 73000,

        #endregion

        #region External Api (от 80000 до 90000)

        ApiEnabled = 80001,

        #endregion

        /// <summary>
        /// Анкета для про консультанта
        /// </summary>
        QuestionnaireForConsultant = 99999,

        #region Права на первичные документы

        /// <summary>
        /// Просмотр всех актов в покупках
        /// </summary>
        ViewAllStatementsBuying = 1110001,

        /// <summary>
        /// Просмотр всех накладных в покупках
        /// </summary>
        ViewAllWaybillsBuying = 1110002,

        /// <summary>
        /// Просмотр всех счетов-фактур в покупках
        /// </summary>
        ViewAllInvoicesBuying = 1110003,

        /// <summary>
        /// Просмотр всех авансовых отчетов в покупках
        /// </summary>
        ViewAllAdvanceStatementsBuying = 1110004,
        
        /// <summary> Просмотр всех УПД в покупках </summary>
        ViewAllUpdsBuying = 1110005,

        /// <summary>
        /// Просмотр собственных актов в покупках
        /// </summary>
        ViewPersonalStatementsBuying = 1120001,

        /// <summary>
        /// Просмотр собственных накладных в покупках
        /// </summary>
        ViewPersonalWaybillsBuying = 1120002,

        /// <summary>
        /// Просмотр собственных счетов-фактур в покупках
        /// </summary>
        ViewPersonalInvoicesBuying = 1120003,

        /// <summary>
        /// Просмотр собственных авансовых отчетов в покупках
        /// </summary>
        ViewPersonalAdvanceStatementsBuying = 1120004,
        
        /// <summary> Просмотр собственных УПД в покупках </summary>
        ViewPersonalUpdsBuying = 1120005,

        /// <summary>
        /// Редактирование всех актов в покупках
        /// </summary>
        EditAllStatementsBuying = 1130001,

        /// <summary>
        /// Редактирование всех накладных в покупках
        /// </summary>
        EditAllWaybillsBuying = 1130002,

        /// <summary>
        /// Редактирование всех счетов-фактур в покупках
        /// </summary>
        EditAllInvoicesBuying = 1130003,

        /// <summary>
        /// Редактирование всех авансовых отчетов в покупках
        /// </summary>
        EditAllAdvanceStatementsBuying = 1130004,
        
        /// <summary> Редактирование всех УПД в покупках </summary>
        EditAllUpdsBuying = 1130005,

        /// <summary>
        /// Редактирование собственных актов в покупках
        /// </summary>
        EditPersonalStatementsBuying = 1140001,

        /// <summary>
        /// Редактирование собственных накладных в покупках
        /// </summary>
        EditPersonalWaybillsBuying = 1140002,

        /// <summary>
        /// Редактирование собственных счетов-фактур в покупках
        /// </summary>
        EditPersonalInvoicesBuying = 1140003,

        /// <summary>
        /// Редактирование собственных авансовых отчетов в покупках
        /// </summary>
        EditPersonalAdvanceStatementsBuying = 1140004,
        
        /// <summary> Редактирование собственных УПД в покупках </summary>
        EditPersonalUpdsBuying = 1140005,

        /// <summary>
        /// Просмотр всех счетов в продажах
        /// </summary>
        ViewAllBillsSales = 1150001,

        /// <summary>
        /// Просмотр всех актов в продажах
        /// </summary>
        ViewAllStatementsSales = 1150002,

        /// <summary>
        /// Просмотр всех накладных в продажах
        /// </summary>
        ViewAllWaybillsSales = 1150003,

        /// <summary>
        /// Просмотр всех счетов-фактур в продажах
        /// </summary>
        ViewAllInvoicesSales = 1150004,

        /// <summary>
        /// Просмотр всех отчетов о розничной продаже
        /// </summary>
        ViewAllRetailReportsSales = 1150005,

        /// <summary>
        /// Просмотр всех отчетов посредника в продажах
        /// </summary>
        ViewAllMiddlemanReportsSales = 1150006,
        
        /// <summary> Просмотр всех УПД в продажах </summary>
        ViewAllUpdsSales = 1150007,

        /// <summary>
        /// Просмотр собственных счетов в продажах
        /// </summary>
        ViewPersonalBillsSales = 1160001,

        /// <summary>
        /// Просмотр собственных актов в продажах
        /// </summary>
        ViewPersonalStatementsSales = 1160002,

        /// <summary>
        /// Просмотр собственных накладных в продажах
        /// </summary>
        ViewPersonalWaybillsSales = 1160003,

        /// <summary>
        /// Просмотр собственных счетов-фактур в продажах
        /// </summary>
        ViewPersonalInvoicesSales = 1160004,

        /// <summary>
        /// Просмотр собственных отчетов о розничной продаже
        /// </summary>
        ViewPersonalRetailReportsSales = 1160005,

        ViewPersonalMiddlemanReportsSales = 1160006,
        
        /// <summary> Просмотр собственных УПД в продажах </summary>
        ViewPersonalUpdSales = 1160007,

        EditAllBillsSales = 1170001,

        /// <summary>
        /// Редактирование всех актов в продажах
        /// </summary>
        EditAllStatementsSales = 1170002,

        /// <summary>
        /// Редактирование всех накладных в продажах
        /// </summary>
        EditAllWaybillsSales = 1170003,

        /// <summary>
        /// Редактирование всех счетов-фактур в продажах
        /// </summary>
        EditAllInvoicesSales = 1170004,

        /// <summary>
        /// Редактирование всех отчетов о розничной продаже
        /// </summary>
        EditAllRetailReportsSales = 1170005,

        /// <summary>
        /// Редактирование всех отчетов посредника в продажах
        /// </summary>
        EditAllMiddlemanReportsSales = 1170006,
        
        /// <summary> Редактирование всех УПД в продажах </summary>
        EditAllUpdsSales = 1170007,

        /// <summary>
        /// Редактирование собственных счетов в продажах
        /// </summary>
        EditPersonalBillsSales = 1180001,

        /// <summary>
        /// Редактирование собственных актов в продажах
        /// </summary>
        EditPersonalStatementsSales = 1180002,

        /// <summary>
        /// Редактирование собственных накладных в продажах
        /// </summary>
        EditPersonalWaybillsSales = 1180003,

        /// <summary>
        /// Редактирование собственных счетов-фактур в продажах
        /// </summary>
        EditPersonalInvoicesSales = 1180004,

        /// <summary>
        /// Редактирование собственных отчетов о розничной продаже
        /// </summary>
        EditPersonalRetailReportsSales = 1180005,

        /// <summary>
        /// Редактирование собственных отчетов посредника в продажах
        /// </summary>
        EditPersonalMiddlemanReportsSales = 1180006,

        /// <summary> Редактирование собственных УПД в продажах </summary>
        EditPersonalUpdsSales = 1180007,

        /// <summary>
        /// Доступ к реестру документов в покупках
        /// </summary>
        PrimaryDocumentsRegisterBuying = 1180100,

        /// <summary>
        /// Доступ к реестру документов в продажах
        /// </summary>
        PrimaryDocumentsRegisterSales = 1180101,

        #endregion

        #region Операции с ЭП для сотрудников банков-партнеров (от 61000 до 61100)
        
        /// <summary>
        /// Оформление заявки на выпуск электронной подписи
        /// </summary>
        RegistrationOfEdsByBank = 61000

        #endregion
    }
}
