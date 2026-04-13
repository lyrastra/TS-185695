using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Email
{
    /// <summary>
    /// Имя enum-a соответствует шаблону письма в md-spam
    /// Description - тема письма
    /// В новом коде можно брать тему через GetSubject()
    /// </summary>
    public enum UnionEmailMarker
    {
        Undefined = 0,

        [Description("Успешная регистрация на вебинар")]
        OnlineTvEventRegistration = 1,

        [Description("Регистрация на курс в учебном центре")]
        OnlineTvLearningCenterRegistration = 2,

        [Description("Запрос на изменение пароля")]
        PasswordRecovery = 3,

        /// <summary>
        /// Отклонение отправки отчёта в контролирующий орган"
        /// {0} - наименование отчёта
        /// </summary>
        [Description("Ваш отчет «{0}» был отклонен")]
        AdministratorRejectDocument = 6,

        /// <summary>
        /// Отправка отчёта в контролирующий орган
        /// {0} - наименование отчёта
        /// {1} - контролирующий орган
        /// </summary>
        [Description("Ваш отчет «{0}» был передан в «{1}»")]
        SendDocument = 7,

        /// <summary>
        /// Получен результат отчёта из контролирующего органа 
        /// {0} - наименование отчёта
        /// </summary>
        [Description("Ваш отчет «{0}» обработан")]
        ServiceProccessedDocument = 8,

        /// <summary> 
        /// Напоминание о сроке оплаты сервиса 
        /// </summary>
        [Description("Ваш расчетный период подходит к концу")]
        AboutExpirationPayment = 9,

        /// <summary> 
        /// Обращение в службу консалтинга
        /// </summary>
        [Description("Ваш запрос в службу консалтинга принят")]
        ConsultationsRequest = 10,

        /// <summary>
        /// Подготовлен ответ на запрос в службу консалтинга
        /// </summary>
        [Description("Ответ на Ваш запрос службой консалтинга")]
        ConsultationsAnswer = 11,

        [Description("Электронная подпись готова к использованию")]
        AboutEdsCreated = 12,

        [Description("Выпуск электронной подписи невозможен")]
        AboutEdsReject = 13,

        /// <summary> 
        /// Для выпуска Вашей ЭЦП необходимо исправить данные 
        /// </summary>
        [Description("Выпуск электронной подписи задержан")]
        AboutEdsDelayed = 14,

        /// <summary> 
        /// Для сдачи отчетности в ПФР необходимо загрузить исправленные документы 
        /// </summary>
        [Description("Соглашение с ПФР НЕ прошло проверку")]
        AboutPfrDocuments = 15,

        /// <summary> 
        /// Получено письмо из {fundname}
        /// fundname - ПФР/ФНС/РОССТАТ
        ///  </summary>
        [Description("Получено письмо из {0}")]
        TextNotificationAboutNewLetter = 18,

        /// <summary> Смена логина пользователя </summary>
        [Description("От Вас получен запрос на изменение логина")]
        MailLoginChanging = 24,

        /// <summary> Логин изменён </summary>
        [Description("Изменение логина")]
        MailLoginWasChanged = 25,

        /// <summary> Регистрация пользователя партнёром </summary>
        [Description("Активация доступа к бухгалтерскому сервису «Моё дело»")]
        MailRegistrationCompletePartner = 26,

        /// <summary> Продление ЭЦП </summary>
        [Description("Срок действия Вашей электронной подписи подходит к концу")]
        ProlongationEds = 27,

        /// <summary> Подтверждение документов ПФР для ЭО с выпущенной ЭЦП </summary>
        [Description("Соглашение с ПФР подтверждено")]
        UserPfrWithSignatureApplied = 29,

        /// <summary> Подтверждение документов ПФР для ЭО без выпущенной ЭЦП </summary>
        [Description("Соглашение с ПФР подтверждено")]
        UserPfrWithNoSignatureApplied = 30,

        /// <summary> Документы для ФНС загружены </summary>
        [Description("Электронная отчётность в ФНС")]
        ElectronicReportFnsUploadBody = 33,

        /// <summary> 
        /// Письмо с счетами, актами, накладными 
        /// Тему письма заполняет сам пользователь
        /// </summary>
        AccountingSendDocumentByMail = 37,

        /// <summary> 
        /// Welcome-письмо при регистрации пользователя Биз без пароля 
        /// </summary>
        [Description("Вам предоставлен бесплатный доступ к интернет-бухгалтерии")]
        WelcomeLetterBizOutLk = 38,

        /// <summary> 
        /// Счет на оплату сервиса - «{serviceName}»
        /// serviceName - МояБухгалтерия Онлайн/Моё дело
        /// </summary>
        [Description("Счет на оплату сервиса - «{0}»")]
        BillingBuyBill = 39,

        /// <summary>
        /// Упрощенный вариант счета без вложений и QR-кода
        /// serviceName - МояБухгалтерия Онлайн/Моё дело
        /// </summary>
        [Description("Счет на оплату сервиса - «{0}»")]
        BillingBuyBillSimplified = 40,

        /// <summary> По Вашему обращению создан тикет </summary>
        [Description("Ваш запрос в обработке")]
        YoutrackIssueCreate = 44,

        /// <summary>
        ///  Вашему запросу {ticketNumber} установлен срок решения 
        /// ticketNumber - номер задачи TS-11232
        /// </summary>
        [Description("Вашему запросу {0} установлен срок решения")]
        YoutrackIssueDueDateSet = 45,

        /// <summary>
        ///  Вашему запросу {ticketNumber} установлен новый срок решения 
        /// ticketNumber - номер задачи TS-11232
        /// </summary>
        [Description("Вашему запросу {0} установлен новый срок решения")]
        YoutrackIssueNewDueDateSet = 46,

        /// <summary> 
        /// Результат запроса {ticketNumber}
        /// ticketNumber - номер задачи TS-11232
        /// </summary>
        [Description("Результат запроса {0}")]
        YoutrackIssueFixed = 47,

        /// <summary> 
        /// Результат запроса {ticketNumber}
        /// ticketNumber - номер задачи TS-11232
        /// </summary>
        [Description("Результат запроса {0}")]
        YoutrackIssueClosed = 48,

        /// <summary> 
        /// Welcome-письмо при регистрации пользователя партнёром 
        /// {partnerName} предоставил Вам доступ к сервису «{serviceName}»
        /// partnerName - имя партнёра Альфабанк/Cбербанк (на текущий момент заполняется пустой строкой)
        /// serviceName - Моё дело/Бюро бухгалтера
        /// Вам предоставлен доступ к сервису «Моя Бухгалтерия Онлайн»
        /// </summary>
        [Description("{0} предоставил Вам доступ к сервису «{1}»")]
        WelcomeLetterPartners = 52,

        /// <summary> 
        /// Welcome-письмо стандартная регистрация с паролем
        /// Отправляется через TDC
        /// </summary>
        WelcomeLetterTestA = 53,

        /// <summary> 
        /// Welcome-письмо при регистрации пользователя Аутсорсинг
        /// Отправляется через TDC
        /// </summary>	
        WelcomeLetterOut = 56,

        /// <summary> 
        /// Welcome-письмо при регистрации пользователя Бюро 
        /// Отправляется через TDC
        /// </summary>	
        WelcomeLetterBuro = 57,

        /// <summary> 
        /// Уведомление о проверке ПФР на наличие соглашения об обмене электронными документами 
        /// </summary>
        [Description("Срочно! Отчет ПФР.")]
        PfrAgreementCheck = 58,

        /// <summary>
        /// Ваш отчет «{document.Name}» был передан в «{directionName}»
        /// directionName - контролирующий орган
        /// </summary>
        [Description("Ваш отчет «{0}» был передан в «{1}»")]
        NeformalDocumentCreated = 59,

        /// <summary>Смена номера телефона, зарегистрированного в сервисе «{ServiceName}»
        /// ServiceName: Моя Бухгалтерия/Моё дело
        /// </summary>
        [Description("Смена номера телефона, зарегистрированного в сервисе «{0}»")]
        MailConfirmPhone = 60,

        /// <summary> 
        /// напоминания о календарных событиях 
        /// Тема: Напоминание: {название календарного события c датой завершения}
        /// </summary>
        CalendarEvents = 61,

        /// <summary> 
        /// Письмо для совместной программы с Альфабанком
        /// Отправляется через TDC
        /// </summary>	
        WelcomeLetterAlphaClub = 64,

        /// <summary>
        /// Письмо при регистрации с лендинга вебмастеров
        /// Отправляется через TDC
        /// </summary>
        WelcomeLetterOnlinePartner = 67,

        /// <summary>
        /// Письмо аутсорсам при регистрации по экспресс-проверке
        /// Отправляется через TDC
        /// </summary>
        WelcomeLetterOutExpress = 68,

        /// <summary> 
        /// Письмо пользователю сервиса о том, что некоторые документы были созданы автоматически 
        /// </summary>
        [Description("Уведомление о выставлении документов")]
        PrimaryDocumentsAutoCreationDone = 69,

        /// <summary> 
        /// Письмо контрагенту о том, что пользователь создал для него предварительный комплект документов 
        /// </summary>
        [Description("Уведомление о выставлении документов")]
        KontragentDocumentsAutoCreationDone = 70,

        /// <summary> 
        /// Письмо пользователю-аутсорсеру сервиса о том, что некоторые документы были созданы автоматически 
        /// </summary>
        [Description("Уведомление о выставлении документов")]
        OutsourcePrimaryDocumentsAutoCreationDone = 71,

        /// <summary>
        /// Письмо при регистрации с клуба предпринимателей
        /// Отправляется через TDC
        /// </summary>
        WelcomeLetterClub = 72,

        /// <summary>
        /// Изменения в контрагентах поставленных на контроль
        /// Тема: Зафиксированы изменения в контрагентах ({controlledContractors.Count})
        /// controlledContractors.Count - количество изменений
        /// </summary>
        [Description("Зафиксированы изменения в контрагентах ({0})")]
        OnControlNotify = 73,

        /// <summary>
        /// Запрос выписки у пользователей на аутсорсе (4 письма в течение месяца)
        /// </summary>
        [Description("Важно: мы ждем от Вас документы для закрытия месяца")]
        KayakoGlavuchetEmail1 = 74,

        [Description("Заканчиваются сроки подачи документов!")]
        KayakoGlavuchetEmail2 = 75,

        [Description("Осторожно: могут быть штрафы!")]
        KayakoGlavuchetEmail3 = 76,

        [Description("Срок сбора документов закончился")]
        KayakoGlavuchetEmail4 = 77,

        /// <summary>
        /// Продление триального доступа
        /// {0} - Моё дело/Бюро бухгалтера
        /// </summary>
        [Description("Вам продлен доступ к сервису «{0}»")]
        TrialProlongation = 78,

        /// <summary>
        /// Запрос выписки у пользователей на аутсорсе с нулевой деятельностью
        /// </summary>
        [Description("Напоминаем про документы для закрытия месяца")]
        KayakoGlavuchetEmail0 = 80,

        /// <summary>
        /// Запрос выписки у пользователей на аутсорсе с нулевой деятельностью, включена интеграция
        /// </summary>
        [Description("Напоминаем про документы для закрытия месяца")]
        KayakoGlavuchetIntEmail0 = 81,

        /// <summary>Запрос выписки у пользователей на аутсорсе, включена интеграция</summary>
        [Description("Важно: мы ждем от Вас документы для закрытия месяца")]
        KayakoGlavuchetIntEmail1 = 82,

        /// <summary>
        /// Письмо с сылкой на прайс-лист в складе
        /// {0} - наименование организации
        /// </summary>
        [Description("Товары для заказа от {0}")]
        StockVendibles = 83,

        /// <summary> 
        /// Письмо с пакетом документов на КЭП для оператора сбербанка
        /// Тема: Документы на выпуск ЭП для {OrganizationName} {Inn}
        /// {0} - OrganizationName - наименование организации
        /// {1} - Inn - ИНН
        /// </summary>
        [Description("Документы на выпуск ЭП для {0} {1}")]
        PackageOfEdsDocuments = 85,

        /// <summary> Восстановление пароля для eds-оператора выдачи электронных подписей (сотрудник сбербанка и др.)</summary>
        [Description("Запрос на изменение пароля")]
        PasswordRecoveryForEdsOperator = 86,

        /// <summary> Регистрация eds-оператора</summary>
        [Description("Регистрация")]
        EdsOperatorRegistration = 87,

        /// <summary> Welcome письмо партнёру, зарегистрированному в системе дистанционного обучения</summary>
        [Description("Доступ к дистанционному обучению от Моё дело")]
        WelcomeLetterDlsOnlinePartner = 88,

        /// <summary> 
        /// Письма с инструкциями по ответу на требования (отчетность)
        /// {0} - ФНС/ПФР/РОССТАТ
        /// </summary>
        [Description("Получено письмо из {0}")]
        InstructionLetterForDemand = 89,

        /// <summary> 
        /// Welcome письмо региональному партнёру 
        /// Отправляется из TDC
        /// </summary>
        WelcomeLetterRegionalPartner = 90,

        /// <summary> 
        /// Письмо о создании электронной подписи
        /// </summary>
        [Description("Электронная подпись готова к использованию")]
        AboutEdsCreatedAndNeedSigned = 91,

        /// <summary> 
        /// Ваша ЭЦП готова к использованию и должна быть подписана (Сбербанк ИП 6% без сотрудников)
        /// </summary>
        [Description("Электронная подпись готова к использованию")]
        AboutEdsCreatedAndNeedSignedSberbankIp6WithoutWorkers = 92,

        [Description("Необходимо подписать сертификат")]
        AboutNeedSignCertificate = 93,

        /// <summary> Необходимо подписать сертификат (Сбербанк ИП 6% без сотрудников)</summary>
        [Description("Необходимо подписать сертификат")]
        AboutNeedSignCertificateSberbankIp6WithoutWorkers = 94,

        /// <summary> Первое письмо пользователю УС Сбербанк - бухгалтерия для ИП (БИП-797)</summary>
        [Description("Приветствуем в сервисе «Бухгалтерия для ИП»!")]
        RegistrationSberIp6WithoutWorkers = 95,

        /// <summary>
        /// Период пользования сервисом истекает через 3 дня, будет автоплатёж
        /// </summary>
        [Description("\"Бухгалтерия для ИП\". 3 дня до новых условий")]
        About3DaysLeftToAutoPay = 96,
        
        /// <summary> Продление ЭЦП (для аутсорсинга) </summary>
        [Description("Срок действия Вашей электронной подписи подходит к концу")]
        ProlongationEdsOutsource = 97,

        /// <summary>Уведомление о включении автоплатежей, с детализацией какие именно мастера подключены</summary>
        [Description("Автоплатеж по налогам и взносам")]
        AutoPaymentsTurnedOn = 98,

        /// <summary>
        /// Письмо, не удалось автоматически создать платежные поручения по мастеру Фиксированных взносов
        /// </summary>
        AutoPaymentsFailedFixesPayment = 99,

        /// <summary>
        /// Письмо, не удалось автоматически создать платежные поручения по мастеру авансов УСН
        /// </summary>
        AutoPaymentsFailedUsnAdvancePayment = 100,

        /// <summary>
        /// Письмо, об автоматическом завершении мастера Фиксированных взносов
        /// </summary>
        AutoPaymentsSuccessfulFixesPayment = 101,

        /// <summary>
        /// Письмо, об автоматическом завершении мастерa авансов УСН
        /// </summary>
        AutoPaymentsSuccessfulUsnAdvancePayment = 102,

        /// <summary>
        /// Письмо, об автоматическом завершении мастерa с нулевой суммой (Взносы/Налог платить не нужно)
        /// </summary>
        AutoPaymentsSuccessfulZeroPayment = 103,

        /// <summary>
        /// Письмо, не удалось автоматически создать платежные поручения по мастеру авансов УСН из-за одной ошибки (с описанием ошибки и инструкцией)
        /// </summary>
        AutoPaymentsFailedUsnAdvancePaymentOneErrorDescription = 104,

        /// <summary>
        /// Письмо, не удалось автоматически создать платежные поручения по мастеру Фиксированных взносов из-за одной ошибки (с описанием ошибки и инструкцией)
        /// </summary>
        AutoPaymentsFailedFixesPaymentOneErrorDescription = 105,

        [Description("Моя бухгалтерия Онлайн. 3 дня до перемен")]
        About3DaysLeftToAutoPayMonthMbo = 106,

        [Description("Моя бухгалтерия Онлайн. 3 дня до перемен")]
        About3DaysLeftToAutoPayYearMbo = 107,

        [Description("Приветствуем в сервисе «Моя бухгалтерия Онлайн»!")]
        RegistrationSberMbo = 108,

        /// <summary> 
        /// Рассылка расчетного листа для сотрудников 
        /// Тема: Расчетный листок за {period:MMMM yyyy} г. {organizationName}
        /// </summary>
        [Description("Расчетный листок за {0} г. {1}")]
        PayrollPaySheet = 109,

        /// <summary>
        /// Письмо, об автоматическом завершении мастера Доп. взносов
        /// </summary>
        AutoPaymentsSuccessfulAdditionalPfrPayment = 110,

        /// <summary>
        /// Письмо, не удалось автоматически создать платежные поручения по мастеру Доп. взносов
        /// </summary>
        AutoPaymentsFailedAdditionalPfrPayment = 111,

        /// <summary>
        /// Письмо, не удалось автоматически создать платежные поручения по мастеру Доп. взносов из-за одной ошибки (с описанием ошибки и инструкцией)
        /// </summary>
        AutoPaymentsFailedAdditionalPfrPaymentOneErrorDescription = 112,

        /// <summary> 
        /// Письмо со сгенерированным паролем при регистрации пользователя из мастера выпуска ЭП для сбербанка тариф "Решение для бизнеса"
        /// {0} - ООО/ИП
        /// </summary>
        [Description("Доступ к Бухгалтерии для {0}")]
        PasswordForMoedeloFromEdsWizardToSberbankSolutionForBusinessAccWithWorkers = 113,

        /// <summary> 
        /// Письмо со сгенерированным паролем при регистрации пользователя из мастера выпуска ЭП для сбербанка тариф "Бухгалтерия для ИП УСН 6% лайт" 
        /// </summary>
        [Description("Доступ к Бухгалтерии для {0}")]
        PasswordForMoedeloFromEdsWizardToSberbankAccountantIp6WithoutWorkers = 114,

        /// <summary> 
        /// Письмо со сгенерированным паролем при регистрации пользователя из мастера выпуска ЭП для сбербанка тариф Моя бухгалтерия Онлайн "Максимальный" 
        /// </summary>
        [Description("Доступ к Бухгалтерии для {0}")]
        PasswordForMoedeloFromEdsWizardToSberbankAccountantMax = 115,

        /// <summary> Получен результат отчёта из фнс однако имеются некоторые ошибки. </summary>
        [Description("")]
        ServiceAcceptedDocumentFnsReportUuProcessed = 116,

        /// <summary> Ваш тикет будет рассматриваться в рамках другого тикета. </summary>
        [Description("Изменился номер запроса")]
        YoutrackIssueNumberSet = 117,

        /// <summary> Ваш тикет будет рассматриваться в рамках другого тикета и установлен срок решения. </summary>
        [Description("Вашему запросу присвоен новый номер и установлен срок решения")]
        YoutrackIssueNumberAndDueDateSet = 118,

        [Description("Получен ответ по запросу сверки с ФНС")]
        FnsCheckResponseReceived = 119,

        /// <summary> Напоминание о неотправленных электронных отчетностях </summary>
        [Description("Напоминание: Не отправлен отчет")]
        NotifyAboutUnsentEReport = 120,

        /// <summary> 
        /// Письмо, об получении нового документа по ЭДО 
        /// {0} - наименование котрагента
        /// </summary>
        [Description("Получен документ от {0}")]
        NotificationAboutNewEdmDocument = 121,

        /// <summary> 
        /// Письмо, об настройке связи ЭДО с контрагентом 
        /// {0} - наименование контрагента
        /// </summary>
        [Description("ЭДО с {0} настроен")]
        NotificationAboutEdmWithKontragentConfigured = 122,

        /// <summary> 
        /// Письмо, об получении входящего приглашения на ЭДО 
        /// {0} - наименование контрагента
        /// </summary>
        [Description("Приглашение к ЭДО от {0")]
        NotificationAboutEdmInvitationReceived = 123,

        /// <summary> 
        /// В случае, если исходящий документ, требующий подписания стал подписанным
        /// {0} - наименование контрагента  
        /// </summary>
        [Description("Документ, отправленный {0}, подписан ")]
        EdmSentDocumentSignRequiredSigned = 124,

        /// <summary> 
        /// В случае, если исходящий документ, требующий подписания стал отказанным 
        /// {0} - наименование контрагента  
        /// </summary>
        [Description("Документ, отправленный {0}, не был подписан")]
        EdmSentDocumentSignRequiredRejected = 125,

        /// <summary>
        /// Оповещение об успешной автооплате
        /// </summary>
        [Description("Оплата прошла успешно")]
        AutoRenewalPaymentSuccess = 126,
        /// <summary>
        /// Оповещение об успешной автооплате АУТ
        /// </summary>
        [Description("Оплата прошла успешно")]
        AutoRenewalOutsourcePaymentSuccess = 127,
        /// <summary>
        /// Оповещение об неуспешной автооплате - тариф устарел
        /// </summary>
        [Description("Оплата не состоится: ваш тариф устарел")]
        AutoRenewalPaymentFailureTariffIsOutOfDate = 128,
        /// <summary>
        /// Оповещение об неуспешной автооплате - тариф устарел АУТ
        /// </summary>
        [Description("Оплата не состоится: ваш тариф устарел")]
        AutoRenewalOutsourcePaymentFailureTariffIsOutOfDate = 129,
        /// <summary>
        /// Оповещение об неуспешной автооплате - техническая ошибка
        /// </summary>
        [Description("Оплата не состоялась по техническим причинам")]
        AutoRenewalPaymentFailureTechnicalReason = 130,
        /// <summary>
        /// Оповещение об неуспешной автооплате - техническая ошибка АУТ
        /// </summary>
        [Description("Оплата не состоялась по техническим причинам")]
        AutoRenewalOutsourcePaymentFailureTechnicalReason = 131,
        /// <summary>
        /// Оповещение об неуспешной автооплате - ошибка выставления счёта
        /// </summary>
        [Description("Оплата не состоялась по техническим причинам")]
        AutoRenewalPaymentFailureBillInvoicingFail = 132,
        /// <summary>
        /// Оповещение об неуспешной автооплате - ошибка выставления счёта АУТ
        /// </summary>
        [Description("Оплата не состоялась по техническим причинам")]
        AutoRenewalOutsourcePaymentFailureBillInvoicingFail = 133,
        /// <summary>
        /// Оповещение об неуспешной автооплате - недостаточно средств для списания
        /// </summary>
        [Description("Оплата не состоялась: недостаточно средств для списания")]
        AutoRenewalPaymentFailureInsufficientFunds = 134,
        /// <summary>
        /// Оповещение об неуспешной автооплате - недостаточно средств для списания АУТ
        /// </summary>
        [Description("Оплата не состоялась: недостаточно средств для списания")]
        AutoRenewalOutsourcePaymentFailureInsufficientFunds = 135,
        /// <summary>
        /// Оповещение о предстоящем списании денежных средств
        /// </summary>
        [Description("Оповещение о предстоящем списании денежных средств")]
        AutoRenewalUpcomingPayment = 136,
        /// <summary>
        /// Оповещение о предстоящем списании денежных средств АУТ
        /// </summary>
        [Description("Оповещение о предстоящем списании денежных средств")]
        AutoRenewalOutsourceUpcomingPayment = 137,
        
        /// <summary>
        /// Оповещение об аннулировании документа от контрагента 1С (Мое дело, Главучет)
        /// </summary>
        [Description("Оповещение об аннулировании документа от контрагента 1С (Мое дело, Главучет)")]
        EdmDocumentFrom1CCancelled = 138,
        
        [Description("Cертификат был успешно зарегистрирован в контролирующих органах")]
        AboutCertificateRegistered = 139,

        /// <summary> 
        /// Оповещение о получении файлов для расшифрования или подписания
        /// </summary>
        [Description("Поступили файлы на подпись (или расшифрование)")]
        EdsCryptoTaskIncoming = 140,

        /// <summary> Продление ЭЦП </summary>
        [Description("Срок действия Вашей электронной подписи подходит к концу")]
        ProlongationEdsUsbToken= 141,

        /// <summary> Продление ЭЦП (для аутсорсинга) </summary>
        [Description("Срок действия Вашей электронной подписи подходит к концу")]
        ProlongationEdsOutsourceUsbToken = 142,

        /// <summary>
        /// Письмо о завершении приобретения услуги Моя бухгалтерия онлайн
        /// </summary>
        [Description("Письмо о завершении приобретения услуги Моя бухгалтерия онлайн")]
        SberMboRegistrationEmail = 143,

        /// <summary>
        /// Регистрация: письмо установки пароля
        /// </summary>
        [Description("Задайте пароль для входа в личный кабинет")]
        SimpleRegistrationLetter = 144,

        /// <summary>
        /// Активация нового тарифа бюро
        /// </summary>
        [Description("Для вас активирован доступ к сервису «Моё дело Бюро»")]
        BuroActivatedTariffLetter = 145,

        /// <summary>
        /// Рассылки Финансиста (AP-8314): Новая заявка на подписание
        /// </summary>
        [Description("Новая заявка на подписание")]
        FinControlNewSigningRequest = 146,
        /// <summary>
        /// Рассылки Финансиста (AP-8314): Заявка на платеж согласована
        /// </summary>
        [Description("Заявка на платеж согласована")]
        FinControlPaymentRequestApproved = 147,
        /// <summary>
        /// Рассылки Финансиста (AP-8314): Заявка на платеж отклонена (автору)
        /// </summary>
        [Description("Заявка на платеж отклонена")]
        FinControlPaymentRequestRejected = 148,
        /// <summary>
        /// Рассылки Финансиста (AP-8314): Заявка на платеж изменена (автору)
        /// </summary>
        [Description("Заявка на платеж изменена")]
        FinControlPaymentRequestChanged = 149,
        /// <summary>
        /// Рассылки Финансиста (AP-8314): Заявка на платеж оплачена
        /// </summary>
        [Description("Заявка на платеж оплачена")]
        FinControlPaymentRequestPaid = 150,
        /// <summary>
        /// Оповещение, что пароль изменён
        /// </summary>
        [Description("Ваш пароль изменён")]
        PasswordChanged = 151,

        /// <summary>
        /// Оповещение об отзыве электронной подписи
        /// </summary>
        [Description("Оповещение об отзыве электронной подписи")]
        EdsRevoked = 152,

        /// <summary> 
        /// Welcome-письмо при регистрации пользователя ИП - Регистрации бизнеса 
        /// Отправляется через TDC
        /// </summary>	
        WelcomeLetterIpRegistration = 153,

        /// <summary> 
        /// Welcome-письмо при регистрации пользователя ООО - Регистрации бизнеса 
        /// Отправляется через TDC
        /// </summary>	
        WelcomeLetterOooRegistration = 154,

        /// <summary>
        /// До истечения платежа ИБ осталось 30 дней
        /// </summary>
        AboutExpirationBizPayment30Days = 155,

        /// <summary>
        /// До истечения платежа ИБ осталось 60 дней
        /// </summary>
        AboutExpirationBizPayment60Days = 156,

        /// <summary>
        /// До истечения платежа ИБ осталось 90 дней
        /// </summary>
        AboutExpirationBizPayment90Days = 157,
        
        /// <summary> 
        /// Оповещение при получении запроса на аннулирование документа
        /// </summary>
        [Description("Поступил запрос на аннулирование документа")]
        EdmDocumentCancellationRequestReceived = 158,
        
        /// <summary> 
        /// Оповещение при отказе в аннулировании со стороны контрагента
        /// </summary>
        [Description("Документ не был аннулирован")]
        EdmDocumentCancellationRejected = 159,
        
        /// <summary> 
        /// Оповещение при успешном аннулировании документа
        /// </summary>
        [Description("Документ аннулирован")]
        EdmDocumentCancelled = 160,
        
        /// <summary> 
        /// Оповещение об ошибке в документообороте (входящий документ)
        /// </summary>
        [Description("Документооборот завершился ошибкой")]
        AboutEdmIncomingDocumentError = 161,
        
        /// <summary> 
        /// Оповещение об ошибке в документообороте (исходящий документ)
        /// </summary>
        [Description("Документооборот завершился ошибкой")]
        AboutEdmOutgoingDocumentError = 162,
        
        /// <summary>
        /// Получен финальный ответ от ФНС по сверке
        /// </summary>
        [Description("Получен ответ от ФНС по запросу сверки")]
        AboutReceivingDocflowIonResponse = 163,
        
        /// <summary>
        /// Получен финальный ответ от ФНС по сверке требующий расшифрования
        /// </summary>
        [Description("Получен ответ от ФНС по запросу сверки")]
        AboutReceivingDocflowIonResponseRequiredDecryption = 164,
        
        /// <summary>
        /// (Триггер по КЗ) Поступило нерасшифрованное письмо
        /// </summary>
        [Description("(Триггер по КЗ) Поступило нерасшифрованное письмо")]
        AboutReceivingEncryptedLetter = 165,
        
        /// <summary>
        /// (Триггер по КЗ) Поступило нерасшифрованное письмо из ФНС
        /// </summary>
        [Description("(Триггер по КЗ) Поступило нерасшифрованное письмо из ФНС")]
        AboutReceivingEncryptedFnsLetter = 166,
        
        /// <summary>
        /// Поступило расшифрованное письмо
        /// </summary>
        [Description("Поступило расшифрованное письмо")]
        AboutReceivingLetter = 167,
        
        /// <summary>
        /// Отказ в приёме письма (отказ расшифрован)
        /// </summary>
        [Description("Отказ в приёме письма (отказ расшифрован)")]
        AboutReceivingLetterRejected = 168,
        
        /// <summary>
        /// (Триггер по КЗ) Отказ в приёме письма (отказ НЕ расшифрован)
        /// </summary>
        [Description("(Триггер по КЗ) Отказ в приёме письма (отказ НЕ расшифрован)")]
        AboutOutgoingLetterRejectIsNotDecrypted = 169,
        
        /// <summary>
        /// Ошибка при подтверждении требования
        /// </summary>
        [Description("Ошибка при подтверждении требования")]
        AboutReceivingDemandConfirmationError = 170,
        
        /// <summary>
        /// (Триггер по КЗ) Поступило нерасшифрованное требование
        /// </summary>
        [Description("(Триггер по КЗ) Поступило нерасшифрованное требование")]
        AboutReceivingEncryptedDemand = 171,
        
        /// <summary>
        /// Поступило расшифрованное требование
        /// </summary>
        [Description("Поступило расшифрованное требование")]
        AboutReceivingDemand = 172,
        
        /// <summary>
        /// Регистрация на промо тариф клиента МБО
        /// </summary>
        [Description("Промо. Приветствуем в сервисе «Моя бухгалтерия Онлайн»!")]
        PromoRegistrationSberMbo = 173,
        
        /// <summary>
        /// Поступило расшифрованное требование (дата отправки требования >= 05.02.2025)
        /// </summary>
        [Description("Поступило расшифрованное требование (дата отправки требования >= 05.02.2025)")]
        AboutReceivingAutoconfirmedDemand = 174,
        
        /// <summary>
        /// (Триггер по КЗ) Поступило нерасшифрованное требование
        /// </summary>
        [Description("(Триггер по КЗ) Поступило нерасшифрованное требование (дата КЗ >= 05.02.2025)")]
        AboutReceivingEncryptedAutoconfirmedDemand = 175,

        /// <summary>
        /// Заявка на платеж, прошедшая маршрут согласования, оплачена
        /// </summary>
        [Description("Заявка на платеж, прошедшая маршрут согласования, оплачена")]
        PaymentRequestPaid = 176,
        
        /// <summary>
        /// Оповещение о включении активного статуса автопродления для аутсорса
        /// </summary>
        [Description("Автоматическое продление тарифа успешно подключено")]
        AutoRenewalStatusActiveOutsource = 177,
        
        /// <summary>
        /// Оповещение о включении активного статуса автопродления
        /// </summary>
        [Description("Автоматическое продление тарифа успешно подключено")]
        AutoRenewalStatusActive = 178,

        /// <summary>
        /// Подтверждение почты в мобильном приложении
        /// </summary>
        [Description("Код подтверждения почты в приложении «Мое дело: бизнес и финансы»")]
        EmailVerificationInMobileApp = 179,
        
        /// Отчёт отклонён
        /// </summary>
        [Description("Отчёт отклонён")]
        ReportRejected = 180,
        
        /// <summary>
        /// Отчёт принят
        /// </summary>
        [Description("Отчёт принят")]
        ReportAccepted = 181,
        
        /// <summary>
        /// Отчёт принят с уведомлением
        /// </summary>
        [Description("Отчёт принят с уведомлением")]
        ReportAcceptedWithNotification = 182,
        
        /// <summary>
        /// Отчёт обработан
        /// </summary>
        [Description("Отчёт обработан")]
        ReportProcessed = 183,
        
        /// <summary>
        /// Отчёт доставлен
        /// </summary>
        [Description("Отчёт доставлен")]
        ReportDelivered = 184,
        
        /// <summary>
        /// Отчёт принят с уведомлением, требуется расшифрование
        /// </summary>
        [Description("Отчёт принят с уведомлением, требуется расшифрование")]
        ReportAcceptedWithNotificationRequiredDecryption = 185,
        
        /// <summary>
        /// Отчёт ФНС принят с уведомлением, требуется расшифрование
        /// </summary>
        [Description("Отчёт ФНС принят с уведомлением, требуется расшифрование")]
        ReportAcceptedWithNotificationRequiredDecryptionFns = 186,
        
        /// <summary>
        /// Отчёт отклонён, требуется расшифрование
        /// </summary>
        [Description("Отчёт отклонён, требуется расшифрование")]
        ReportRejectedRequiredDecryption = 187,
        
        /// <summary>
        /// Отчёт ФНС отклонён, требуется расшифрование
        /// </summary>
        [Description("Отчёт ФНС отклонён, требуется расшифрование")]
        ReportRejectedRequiredDecryptionFns = 188,
        
        /// <summary>
        /// Отчёт отправлен в ведомство
        /// </summary>
        [Description("Отчёт отправлен в ведомство")]
        ReportSent = 189,

        /// <summary>
        /// Отчёт принят частично
        /// </summary>
        [Description("Отчёт принят частично")]
        ReportAcceptedPartly = 190,

        /// <summary>
        /// ЗПЭД в статусе "Не отправлен"
        /// </summary>
        [Description("ЗПЭД в статусе \"Не отправлен\"")]
        PfrEdmStatementNotSigned = 191,

        /// <summary>
        /// Создайте пароль для входа в веб-версию "Моё дело"
        /// </summary>
        [Description("Создайте пароль для входа в веб-версию \"Моё дело\"")]
        SimpleRegistrationLetterMobile = 192,

        /// <summary>
        /// Ваш логин для доступа в сервис "Моё дело"
        /// </summary>
        [Description("Ваш логин для доступа в сервис \"Моё дело\"")]
        MailLoginWasChangedMobile = 193,

        /// <summary>
        /// Welcome-письмо при регистрации пользователя Sovcombank WL
        /// </summary>
        [Description("Приветствуем в сервисе «Бухгалтерия»!")]
        WelcomeLetterSovcombankWlRegistration = 194,

        /// <summary>
        /// Оповещение по СЭДО
        /// </summary>
        [Description("Оповещение по СЭДО")]
        SedoNotification = 195,

        /// <summary>
        /// Welcome письмо при регистрации пользователя WbBank White Label
        /// </summary>
        [Description("Добро пожаловать в «Моё дело»!")]
        WelcomeLetterWbBankRegistration = 196,
        
        /// <summary>
        /// Оповещение о предстоящем списании денежных средств АУТ (Превышены лимиты)
        /// </summary>
        [Description("Оповещение о предстоящем списании денежных средств (Превышены лимиты)")]
        AutoRenewalOutsourceUpcomingPaymentLimitsExcess = 197,
        
        /// <summary>
        /// Оповещение о предстоящем списании денежных средств АУТ (Расчет параметров тарификации не выполнен)
        /// </summary>
        [Description("Автоматическое списание не может быть выполнено")]
        AutoRenewalOutsourceUpcomingPaymentTarifficationFail = 198
    }
}
