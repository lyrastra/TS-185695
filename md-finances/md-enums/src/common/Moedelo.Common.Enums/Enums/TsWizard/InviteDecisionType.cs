using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.TsWizard
{
    public enum InviteDecisionType
    {

        /// <summary>
        /// Спросить Стэк, получали-ли они приглашение
        /// </summary>
        [Description("Необходимо спросить Стэк, получали ли они приглашение")]
        AskStekDidTheyGetInvitation = 1,

        /// <summary>
        /// Пропросить Стэк ускориться
        /// </summary>
        [Description("Попросить Стэк ускориться")]
        AskStekToHandleInvitation = 2,

        /// <summary>
        /// Стэк завершил настройку, но забыл добавить контрагента в адресную книгу
        /// </summary>
        [Description("Стэк завершил настройку, но забыл добавить контрагента в адресную книгу")]
        AskStekToAddKontragentToAddressBook = 4,

        /// <summary>
        /// Стэк завершил настройку, контрагент в адресной книге, но почему-то у нас не настроилась связь. Обычно это признак сфакапившейся консоли
        /// </summary>
        [Description("Необходимо обновить статус приглашения вручную")]
        OperatorHaveToUpdateStatusManually = 5,

        /// <summary>
        /// Не удалось принять решение, что-то странное
        /// </summary>
        [Description("Необходимо создать задачу разработчикам")]
        OperatorHaveToCreateTS = 6,

        /// <summary>
        /// Заявка отправлена через старую роуминговую почту - мы не поддерживаем такие заявки
        /// </summary>
        [Description("Старая заявка, отправленная через роуминговую почту. Переотправьте её")]
        UnsupportedRoamingMail = 7,

        /// <summary>
        /// Задвоенные контрагенты в EdmInvite
        /// </summary>
        [Description("Обнаружены задвоенные контрагенты в EdmInvite")]
        DuplicatesByKontragentDetected = 8,

        /// <summary>
        /// Задвоенные гуиды в EdmInvite
        /// </summary>
        [Description("Обнаружены задвоенные GUID'ы в EdmInvite")]
        DuplicatesByAbnGuidsDetected = 9,
        
        /// <summary>
        /// Ошибка приглашения в стеке
        /// </summary>
        [Description("Ошибка приглашения в стеке")]
        StekInviteError = 10,

        /// <summary>
        /// Нет настройки связи в адресной книге СТЭКа, заведите задачу в Мантиc
        /// </summary>
        [Description("Нет настройки связи в адресной книге СТЭКа, заведите задачу в Мантиc")]
        NoStekErrorsAndNoKontragentInAddressBook = 11,

        /// <summary>
        /// Доступна настроенная связь по другому оператору
        /// </summary>
        [Description("Доступна настроенная связь по другому оператору")]
        SetUpInvitationWithAnotherProvider = 12,

        /// <summary>
        /// Всё должно работать
        /// </summary>
        [Description("Все должно работать")]
        EverythingIsOK = 0
    }
}