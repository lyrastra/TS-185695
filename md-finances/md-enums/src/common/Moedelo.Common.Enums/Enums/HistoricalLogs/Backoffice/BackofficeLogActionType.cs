namespace Moedelo.Common.Enums.Enums.HistoricalLogs.Backoffice
{
    /// <summary> Тип действия пользователя.
    /// Разрешено добавлять новые типы
    /// </summary>
    public enum BackofficeLogActionType : byte
    {
        /// <summary> Выбрать все </summary>
        [BackofficeLogActionTypeInfo("Выбрать все")]
        None = 0,

        /// <summary> Отчёт по продажам 2.0/2.1 </summary>
        [BackofficeLogActionTypeInfo("Отчёт по продажам 2.0/2.1")]
        ReportSales2 = 1,

        /// <summary> Детальный отчёт по продажам 2.0/2.1 </summary>
        [BackofficeLogActionTypeInfo("Детальный отчёт по продажам 2.0/2.1")]
        ReportDetailSales2 = 2,

        /// <summary> Отчёт Список лидов за указанный период </summary>
        [BackofficeLogActionTypeInfo("Отчёт Список лидов за указанный период")]
        ReportLeadList = 3,

        /// <summary> Отчёт о переподписке/Отчет о переподписке Аутсорс </summary>
        [BackofficeLogActionTypeInfo("Отчёт о переподписке/Отчет о переподписке Аутсорс")]
        ReportRenewSubscriptions = 4,

        /// <summary> Отчёт по каналам лидов </summary>
        [BackofficeLogActionTypeInfo("Отчёт по каналам лидов")]
        ReportLeadChannels = 5,

        /// <summary> Отчёт по активности БЮРО </summary>
        [BackofficeLogActionTypeInfo("Отчёт по активности БЮРО")]
        ReportActivityBizBuro = 6,

        /// <summary> Отчет по активности БИЗ (триальные карты) </summary>
        [BackofficeLogActionTypeInfo("Отчёт по активности БИЗ (триальные карты)")]
        ReportActivityBizTrialCard = 7,

        /// <summary> Отчёт по начислениям Аутсорсинга </summary>
        [BackofficeLogActionTypeInfo("Отчёт по начислениям Аутсорсинга")]
        ReportOutsourcingAccrual = 8,

        /// <summary> Отчёт по выручке </summary>
        [BackofficeLogActionTypeInfo("Отчёт по выручке")]
        ReportProceeds = 9,

        /// <summary> Отчёт по аутсорсингу (по главному аутсорсеру) </summary>
        [BackofficeLogActionTypeInfo("Отчёт по аутсорсингу (по главному аутсорсеру)")]
        ReportOutsourceForProfessionalOutsourcer = 10,

        /// <summary> Отчёт по аутсорсингу (по бухгалтеру) </summary>
        [BackofficeLogActionTypeInfo("Отчёт по аутсорсингу (по бухгалтеру)")]
        ReportOutsourceForAccountant = 11,

        /// <summary> Отчёт по аутсорсингу (по клиенту) </summary>
        [BackofficeLogActionTypeInfo("Отчёт по аутсорсингу (по клиенту)")]
        ReportOutsourceForClient = 12,

        /// <summary> Новый отчёт по активности </summary>
        [BackofficeLogActionTypeInfo("Новый отчёт по активности")]
        ReportNewActivity = 13,

        /// <summary> Отчёт по не учитываемым в статистике логинам </summary>
        [BackofficeLogActionTypeInfo("Отчёт по не учитываемым в статистике логинам")]
        ReportInternalFirms = 14,

        /// <summary> Отчёт по статусам </summary>
        [BackofficeLogActionTypeInfo("Отчёт по статусам")]
        ReportStatuses = 15,

        /// <summary> Отчёт Выгрузка базы E-mail </summary>
        [BackofficeLogActionTypeInfo("Отчёт Выгрузка базы E-mail")]
        ReportEmails = 16,

        /// <summary> Отчёт по анкете </summary>
        [BackofficeLogActionTypeInfo("Отчёт по анкете")]
        ReportQuestionnaire = 17,

        /// <summary> Отчёт по анкете ПРО </summary>
        [BackofficeLogActionTypeInfo("Отчёт по анкете ПРО")]
        ReportQuestionnairePro = 18,

        /// <summary> Отчёт Продажи и клиентская база </summary>
        [BackofficeLogActionTypeInfo("Отчёт Продажи и клиентская база")]
        ReportSalesAndClients = 19,

        /// <summary> Отчёт Каналы лидов 2/Каналы лидов 2 (новый) </summary>
        [BackofficeLogActionTypeInfo("Отчёт Каналы лидов 2/Каналы лидов 2 (новый)")]
        ReportLeadChannels2 = 20,

        /// <summary> Отчёт Закрывающие документы </summary>
        [BackofficeLogActionTypeInfo("Отчёт Закрывающие документы")]
        ReportClosingDocuments = 21,

        /// <summary> Отчёт Акты и закрывающие документы в 1С (Акты) </summary>
        [BackofficeLogActionTypeInfo("Отчёт Акты и закрывающие документы в 1С (Акты)")]
        ReportActs = 22,

        /// <summary> Отчёт Акты и закрывающие документы в 1С (Платежки) </summary>
        [BackofficeLogActionTypeInfo("Отчёт Акты и закрывающие документы в 1С (Платежки)")]
        ReportPayments = 23,

        /// <summary> Отчёт Не выгруженные акты в 1С </summary>
        [BackofficeLogActionTypeInfo("Отчёт Не выгруженные акты в 1С")]
        ReportActsWithoutSetIsDownloaded = 24,

        /// <summary> Отчёт План-факт </summary>
        [BackofficeLogActionTypeInfo("Отчёт План-факт")]
        ReportPlanFact = 25,

        /// <summary>Сводный отчёт о переподписке/Сводный отчет о переподписке Аутсорс</summary>
        [BackofficeLogActionTypeInfo("Сводный отчёт о переподписке/Сводный отчет о переподписке Аутсорс")]
        ReportRenewSubscriptionsSummary = 26,

        /// <summary>Отчёт о сроке подписки</summary>
        [BackofficeLogActionTypeInfo("Отчёт о сроке подписки")]
        ReportSubscriptionPeriod = 27,

        /// <summary>Отчёт по активным Триалам</summary>
        [BackofficeLogActionTypeInfo("Отчет по активным Триалам")]
        ReportActiveTrials = 28,

        /// <summary>Отчёт по сверке с выписками банка</summary>
        [BackofficeLogActionTypeInfo("Отчёт по сверке с выписками банка")]
        ReportCheckWithBanks = 29,

        /// <summary>Отчёт по сверке с выписками электронных платежных систем</summary>
        [BackofficeLogActionTypeInfo("Отчёт по сверке с выписками электронных платежных систем")]
        ReportCheckWithAcquiring = 30,

        /// <summary>Отчёт по активным Фримиумам</summary>
        [BackofficeLogActionTypeInfo("Отчет по активным Фримиумам")]
        ReportActiveFreemiums = 31,

        /// <summary>Отчёт по сверке биллинга с самим биллингом</summary>
        [BackofficeLogActionTypeInfo("Отчёт по сверке биллинга с самим биллингом")]
        ReportCheckWithBilling = 32,

        /// <summary>Отчёт по активным платным тарифам</summary>
        [BackofficeLogActionTypeInfo("Отчёт по активным платным тарифам")]
        ReportActivePayableTariffs = 33,

        /// <summary> Отчёт по выставленным счетам </summary>
        [BackofficeLogActionTypeInfo("Отчёт по выставленным счетам")]
        ReportCreateBill = 34,

        /// <summary> Отчёт по активности БИЗ/БЮРО для партнеров </summary>
        [BackofficeLogActionTypeInfo("Отчёт по активности БИЗ/БЮРО для партнеров")]
        ReportActivityBizBuroForPartners = 35,

        /// <summary> Сводный отчёт о переподписке/Сводный отчет о переподписке Аутсорс для партнеров </summary>
        [BackofficeLogActionTypeInfo("Сводный отчёт о переподписке/Сводный отчет о переподписке Аутсорс для партнеров")]
        ReportRenewSubscriptionsSummaryForPartners = 36,

        /// <summary> Отчёт о переподписке/Отчёт о переподписке Аутсорс для партнеров </summary>
        [BackofficeLogActionTypeInfo("Отчёт о переподписке/Отчёт о переподписке Аутсорс для партнеров")]
        ReportRenewSubscriptionsForPartners = 37,

        /// <summary> Отчёт по выставленным счетам для партнеров </summary>
        [BackofficeLogActionTypeInfo("Отчёт по выставленным счетам для партнеров")]
        ReportCreateBillForPartners = 38,
        //До 63 включительно - секция на отчеты

        /// <summary> Просмотр биллинга фирмы </summary>
        [BackofficeLogActionTypeInfo("Просмотр биллинга фирмы", BackOfficeLogObjectType.Firm)]
        ViewFirmBilling = 65,

        /// <summary> Просмотр подробной информации о фирме </summary>
        [BackofficeLogActionTypeInfo("Просмотр подробной информации о фирме")]
        ViewFirmAdditionalInfo = 66,

        /// <summary> Просмотр регистрационных данных пользователя </summary>
        [BackofficeLogActionTypeInfo("Просмотр регистрационных данных пользователя", BackOfficeLogObjectType.User)]
        ViewUserRegistrationInfo = 67,

        /// <summary> Просмотр реквизитов фирмы </summary>
        [BackofficeLogActionTypeInfo("Просмотр реквизитов фирмы", BackOfficeLogObjectType.Firm)]
        ViewFirmRequisites = 68,

        /// <summary> Просмотр прав пользователя </summary>
        [BackofficeLogActionTypeInfo("Просмотр прав пользователя", BackOfficeLogObjectType.User)]
        ViewUserPermisiions = 69,

        /// <summary> Просмотр списка партнеров </summary>
        [BackofficeLogActionTypeInfo("Просмотр списка партнеров")]
        ViewPartnerList = 70,

        /// <summary> Просмотр списка операторов </summary>
        [BackofficeLogActionTypeInfo("Просмотр списка операторов")]
        ViewOperatorList = 71,

        //До 95 включительно - секция на просмотр

        [BackofficeLogActionTypeInfo("Редактирование прайс-листов")]
        ChangePriceLists = 97,

        /// <summary> Выключение интеграции клиенту </summary>
        [BackofficeLogActionTypeInfo("Выключение интеграции клиенту")]
        DisableIntegration = 98,

        /// <summary> Закрепление клиента за оператором </summary>
        [BackofficeLogActionTypeInfo("Закрепление клиента за оператором", BackOfficeLogObjectType.Firm)]
        FixateСlientToOperator = 99,

        /// <summary> Закрепление клиента за партнером </summary>
        [BackofficeLogActionTypeInfo("Закрепление клиента за партнером", BackOfficeLogObjectType.Firm)]
        FixateСlientToPartner = 100,

        /// <summary> Открепление клиента </summary>
        [BackofficeLogActionTypeInfo("Открепление клиента", BackOfficeLogObjectType.Firm)]
        UnfixateСlient = 101,

        /// <summary> Смена комментария клиенту </summary>
        [BackofficeLogActionTypeInfo("Смена комментария клиенту", BackOfficeLogObjectType.Firm)]
        ChangeСlientComment = 102,

        /// <summary> Смена продавца платежа </summary>
        [BackofficeLogActionTypeInfo("Смена продавца платежа", BackOfficeLogObjectType.Payment)]
        ChangePaymentSaler = 103,

        /// <summary> Автоматическое распознавание при распознавании выписок </summary>
        [BackofficeLogActionTypeInfo("Автоматическое распознавание при распознавании выписок",
            BackOfficeLogObjectType.Payment)]
        RecognizePaymentAutomatically = 104,

        /// <summary> Ручное распознавание при распознавании выписок </summary>
        [BackofficeLogActionTypeInfo("Ручное распознавание при распознавании выписок", BackOfficeLogObjectType.Payment)]
        RecognizePaymentManually = 105,

        /// <summary> Ручное распознавание по частям при распознавании выписок </summary>
        [BackofficeLogActionTypeInfo("Ручное распознавание по частям при распознавании выписок",
            BackOfficeLogObjectType.Payment)]
        RecognizePaymentManuallyByParts = 106,

        /// <summary> Ручное распознавание по идентификатору при распознавании выписок </summary>
        [BackofficeLogActionTypeInfo("Ручное распознавание по идентификатору при распознавании выписок",
            BackOfficeLogObjectType.Payment)]
        RecognizePaymentManuallyById = 107,

        /// <summary> Добавление платежа через биллинг </summary>
        [BackofficeLogActionTypeInfo("Добавление платежа через биллинг", BackOfficeLogObjectType.Payment)]
        AddingPaymentFromBilling = 108,

        /// <summary> Изменение UTM-меток через биллинг </summary>
        [BackofficeLogActionTypeInfo("Изменение UTM-меток через биллинг", BackOfficeLogObjectType.User)]
        ChangeUtmFieldsFromBilling = 109,

        /// <summary> Перенос данных из одной фирмы в другую </summary>
        [BackofficeLogActionTypeInfo("Перенос данных из одной фирмы в другую", BackOfficeLogObjectType.Firm)]
        FirmDataTransfer = 110,

        /// <summary> Редактирование реквизитов партнёра </summary>
        [BackofficeLogActionTypeInfo("Редактирование реквизитов партнёра", BackOfficeLogObjectType.Partner)]
        EditRegionalPartnerRequisites = 111,

        /// <summary> Редактирование ИНН фирмы </summary>
        [BackofficeLogActionTypeInfo("Редактирование ИНН фирмы", BackOfficeLogObjectType.Firm)]
        ChangeFirmInn = 112,

        /// <summary> Редактирование логина пользователя </summary>
        [BackofficeLogActionTypeInfo("Редактирование логина пользователя", BackOfficeLogObjectType.User)]
        ChangeUserLogin = 113,

        /// <summary> Редактирование полного наименования фирмы </summary>
        [BackofficeLogActionTypeInfo("Редактирование реквизитов фирмы", BackOfficeLogObjectType.Firm)]
        ChangeFirmRequisites = 114,

        /// <summary> Редактирование ФИО пользователя </summary>
        [BackofficeLogActionTypeInfo("Редактирование ФИО пользователя", BackOfficeLogObjectType.User)]
        ChangeUserFullName = 115,

        /// <summary> Редактирование телефона при регистрации пользователя </summary>
        [BackofficeLogActionTypeInfo("Редактирование телефона при регистрации пользователя",
            BackOfficeLogObjectType.User)]
        ChangeUserRegistrationPhone = 116,

        /// <summary> Редактирование ОПФ фирмы </summary>
        [BackofficeLogActionTypeInfo("Редактирование ОПФ фирмы", BackOfficeLogObjectType.Firm)]
        ChangeFirmLegalForm = 117,

        /// <summary> Редактирование региона оплаты фирмы </summary>
        [BackofficeLogActionTypeInfo("Редактирование региона оплаты фирмы", BackOfficeLogObjectType.Firm)]
        ChangeFirmPayRegion = 118,

        /// <summary> Редактирование отдела сопровождения фирмы </summary>
        [BackofficeLogActionTypeInfo("Редактирование отдела сопровождения фирмы", BackOfficeLogObjectType.Firm)]
        ChangeFirmSupportDepartment = 119,

        /// <summary> Редактирование права доступа пользователя </summary>
        [BackofficeLogActionTypeInfo("Редактирование права доступа пользователя", BackOfficeLogObjectType.User)]
        ChangeUserAccessRule = 120,

        /// <summary> Закрепление клиента за франчайзи </summary>
        [BackofficeLogActionTypeInfo("Закрепление клиента за франчайзи", BackOfficeLogObjectType.Firm)]
        FixateСlientToFranchisee = 121,

        // <summary> Закрепление клиента за аутсорсинг МД </summary>
        [BackofficeLogActionTypeInfo("Закрепление клиента за аутсорсинг МД", BackOfficeLogObjectType.Firm)]
        FixateСlientToOutsourcingMdPartner = 122,

        /// <summary> Общий отчёт о переподписке </summary>
        [BackofficeLogActionTypeInfo("Общий отчёт о переподписке")]
        ReportGeneralRenewSubscriptions = 123,

        // <summary> Закрепление клиента за проф. аутсорсером </summary>
        [BackofficeLogActionTypeInfo("Закрепление клиента за проф. аутсорсером", BackOfficeLogObjectType.Firm)]
        FixateClientToProfOutsourcer = 124,
        
        /// <summary> Добавление автооплачиваемого платежа </summary>
        [BackofficeLogActionTypeInfo("Добавление автооплачиваемого платежа", BackOfficeLogObjectType.Payment)]
        AddAutoPaidPayment = 125,
        
        /// <summary> Изменение автооплачиваемого платежа </summary>
        [BackofficeLogActionTypeInfo("Изменение автооплачиваемого платежа", BackOfficeLogObjectType.Payment)]
        ModifyAutoPaidPayment = 126,
        
        /// <summary> Отключение автооплачиваемого платежа </summary>
        [BackofficeLogActionTypeInfo("Отключение автооплачиваемого платежа", BackOfficeLogObjectType.Payment)]
        DisableAutoPaidPayment = 127,

        [BackofficeLogActionTypeInfo("Смена системы налогообложения", BackOfficeLogObjectType.Firm)]
        TransferUsnOsno = 128,

        // <summary> Изменение аккаунта фирмы </summary>
        [BackofficeLogActionTypeInfo("Изменение аккаунта фирмы", BackOfficeLogObjectType.Firm)]
        FirmAccountChanged = 129,

        /// <summary> Отчёт по остаткам на р/с </summary>
        [BackofficeLogActionTypeInfo("Отчёт по остаткам на р/с")]
        ReportAccountRemains = 130,

        /// <summary> Редактирование пароля пользователя </summary>
        [BackofficeLogActionTypeInfo("Редактирование пароля пользователя", BackOfficeLogObjectType.User)]
        ChangeUserPassword = 131,

        /// <summary> Выпуск временного пароля пользователя </summary>
        [BackofficeLogActionTypeInfo("Выпуск временного пароля пользователя", BackOfficeLogObjectType.User)]
        GenerateUserTemporaryPassword = 132,

        /// <summary> Удаление пользователя </summary>
        [BackofficeLogActionTypeInfo("Удаление пользователя", BackOfficeLogObjectType.User)]
        MarkUserAsDeleted = 133,

        /// <summary> Смена флага "Не учитывать в статистике" </summary>
        [BackofficeLogActionTypeInfo("Изменение флага учета в статистике", BackOfficeLogObjectType.Firm)]
        ChangeIsInternalFlag = 134
    }
}