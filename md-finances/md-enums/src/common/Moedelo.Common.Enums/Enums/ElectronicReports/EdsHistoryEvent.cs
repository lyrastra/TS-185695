using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.ElectronicReports
{
    public enum EdsHistoryEvent
    {
        /// <summary>Загружены документы на выпуск ЭП</summary>
        [Description("Загружены документы на выпуск ЭП")]
        Upload = 1,
        /// <summary>Документы подтверждены партнёром</summary>
        [Description("Документы подтверждены партнёром")]
        ConfirmPartner = 2,
        /// <summary>Отправлен запрос на создание ЭП</summary>
        [Description("Отправлен запрос на создание ЭП")]
        SendFirstRequest = 3,
        /// <summary>Отправлен запрос на изменение ЭП</summary>
        [Description("Отправлен запрос на изменение ЭП")]
        SendChangeRequest = 4,
        /// <summary>Отправлен запрос на пролонгацию ЭП</summary>
        [Description("Отправлен запрос на пролонгацию ЭП")]
        SendProlongationRequest = 5,
        /// <summary>Отправлен запрос на восстановление ЭП</summary>
        [Description("Отправлен запрос на восстановление ЭП")]
        SendRecoveryRequest = 6,
        /// <summary>Помещён в проблемные</summary>
        [Description("Помещён в проблемные")]
        Problem = 7,
        /// <summary>Запрос отклонён провайдером</summary>
        [Description("Запрос отклонён провайдером")]
        Rejected = 8,
        /// <summary>ЭЦП успешно выпущена</summary>
        [Description("ЭЦП успешно выпущена")]
        Created = 9,
        /// <summary>Отправлено уведомление пользователю об истечении срока действия ЭП</summary>
        [Description("Отправлено уведомление пользователю об истечении срока действия ЭП")]
        AutomaticNotification = 10,
        /// <summary>Отправка автоматического запроса на продление ЭП завершилась с ошибкой</summary>
        [Description("Отправка автоматического запроса на продление ЭП завершилась с ошибкой")]
        AutomaticSendError = 11,
        /// <summary>Отправка запроса на изменение ЭП для сдачи отчётности в Росстат</summary>
        [Description("Отправка запроса на изменение ЭП для сдачи отчётности в Росстат")]
        SendRequestToRosstat = 12,
        /// <summary>Отправка запроса на блокирование старой ЭП</summary>
        [Description("Отправка запроса на блокирование старой ЭП")]
        SendBlockingRequest = 13,
        /// <summary>Информация об ЭП скопирована из другого аккаунта</summary>
        [Description("Информация об ЭП скопирована из другого аккаунта")]
        Copied = 14,
        /// <summary>Информация об ЭП перенесена из другого аккаунта</summary>
        [Description("Информация об ЭП перенесена из другого аккаунта")]
        Moved = 15,
        /// <summary>Отправлено уведомление пользователю об истечении срока действия ЭП со списком ошибок</summary>
        [Description("Отправлено уведомление пользователю об истечении срока действия ЭП со списком ошибок")]
        AutomaticNotificationWithErrors = 16,
        /// <summary>Загружены документы на перевыпуск ЭП</summary>
        [Description("Загружены документы на перевыпуск ЭП")]
        UploadReRequest = 17,
        /// <summary>Подпись включена вручную</summary>
        [Description("Подпись включена вручную")]
        TurnOn = 18,
        /// <summary>Начато прохождение мастера</summary>
        [Description("Начато прохождение мастера")]
        PassingStarted = 19,
        /// <summary>Сертификат подписан</summary>
        [Description("Сертификат подписан")]
        CertificateSigned = 20,
        /// <summary>Сертификат подписан неправильно или отменено в консоли</summary>
        [Description("Сертификат подписан неправильно или отменено в консоли")]
        CertificateRejected = 21,
        /// <summary>Отправлено уведомление пользователю о необходимости подписать сертификат</summary>
        [Description("Отправлено уведомление пользователю о необходимости подписать сертификат")]
        AutomaticNotificationSignCertificate = 22,
        /// <summary>EdsState изменен вручную</summary>
        [Description("EdsState изменен вручную")]
        EdsStateChangedManually = 23,
        /// <summary>EdsState изменен вручную</summary>
        [Description("EdsState изменен вручную")]
        WizardCancelledByUser = 24,
        /// <summary>Сброшена блокировка реквизитов вручную</summary>
        [Description("Сброшена блокировка реквизитов вручную")]
        RequisitesUnlockManually = 25,
        /// <summary>Визард сброшен в изначальное состояние</summary>
        [Description("Визард сброшен в изначальное состояние")]
        ResetToInitial = 26,
        /// <summary>Вызов курьера</summary>
        [Description("Вызов курьера")]
        RequestCourier = 27,
        /// <summary>Заявка отмечена в архиве</summary>
        [Description("Заявка отмечена в архиве")]
        RequestArchived = 28,
        /// <summary>Заявление на выпуск ЭП отправлено через ЭДО</summary>
        [Description("Заявление на выпуск ЭП отправлено через ЭДО")]
        SendStatementViaEdm = 29,
        /// <summary>Завершен трансфер подписи с Астрала на СТЭК</summary>
        [Description("Завершен трансфер подписи с Астрала на СТЭК")]
        AstralToMdTransferComplete = 30,
        /// <summary>Создан бэкап настроек ЭДО связей с контрагентами в EdmInviteTransfer</summary>
        [Description("Создан бэкап настроек ЭДО связей с контрагентами в EdmInviteTransfer")]
        AstralEdmInvitesBackupComplete = 31,
        /// <summary>Удалены все имевшиеся ЭДО связи с контрагентами из EdmInvite</summary>
        [Description("Удалены все имевшиеся ЭДО связи с контрагентами из EdmInvite")]
        AstralEdmInvitesDeleted = 32,
        /// <summary>Удалены заявления из раздела реквизиты -> ЭДО</summary>
        [Description("Удалены заявления из раздела реквизиты -> ЭДО")]
        AstralEdmStatementsDeleted = 33,
        /// <summary>Загружен документ в мастере выпуска ЭП</summary>
        [Description("Загружен документ в мастере выпуска ЭП")]
        EdsWizardDocumentUpload = 34,
        /// <summary>Завершен трансфер подписи с СТЭК на Астрала</summary>
        [Description("Завершен трансфер подписи с СТЭК на Астрала")]
        MdToAstralTransferComplete = 35,
        /// <summary>Шаг визарда регистрации сертификата</summary>
        [Description("Шаг визарда регистрации сертификата")]
        WizardStep = 36,
        /// <summary>Выбор пользователем сертификата</summary>
        [Description("Выбор пользователем сертификата")]
        WizardSelectCertificate = 37,
        /// <summary>Удаление пользователем сертификата</summary>
        [Description("Удаление пользователем сертификата")]
        WizardRemoveCertificate = 38,
        /// <summary>Создание пользователем сертификата</summary>
        [Description("Создание пользователем сертификата")]
        WizardNewCertificate = 39,
        /// <summary>Завершен трансфер подписи с Астрала на токены</summary>
        [Description("Трансфер ЭП с УЦ Астрал на токены")]
        AstralToUsbTokenTransferComplete = 40,
        /// <summary>Завершен трансфер подписи с УЦ МоеДело на токены</summary>
        [Description("Трансфер ЭП с УЦ МоеДело на токены")]
        MoedeloToUsbTokenTransferComplete = 41,
        /// <summary>ЭП отозвана удостоверяющим центром</summary>
        [Description("ЭП отозвана удостоверяющим центром")]
        EdsRevoked = 42,
        /// <summary>Установлен признак авторасшифрования ФНС</summary>
        SetIsFnsAutodecryptionEnabled = 43
    }
}
