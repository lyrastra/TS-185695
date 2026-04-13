using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.TsWizard
{
    /// <summary>
    /// Решения по непрочитанным документам
    /// </summary>
    public enum UnreadDocflowsDecisionType
    {
        /// <summary>
        /// Всё нормально (на будущее, пока-что такой вариант не генерируется)
        /// </summary>
        [Description("Всё нормально")]
        EverythindIsOK = 0,

        /// <summary>
        /// Что-то непонятное, завести ТС
        /// </summary>
        [Description("Необходимо создать задачу разработчикам")]
        RequestOperatorToOpenTs = 1,

        /// <summary>
        /// Переключить на связь с другим провайдером (есть другие связи, по коим есть непрочитанные)
        /// </summary>
        [Description("Существуют другие связи с непрочитанными документами. Надо переключиться на них")]
        RequestOperatorToSetKontragentConnectionWithAnotherEdmSystem = 2,

        /// <summary>
        /// Создать ТС стеку по поводу нехождения документов (нет непрочитанных ни у кого, подразумевается, что пользователь завел ТС не просто так и ждёт докуменита)
        /// </summary>
        [Description("Создать ТС Стеку по поводу неполучения документов")]
        RequestOperatorToOpenTaskInStekAboutDocflowsForOurUsers = 3,

        /// <summary>
        /// Связь ещё не настроена вовсе, решить проблему со связью
        /// </summary>
        [Description("Связь ещё не настроена, решить проблему со связью")]
        ResolveEdmLinkProblem = 4,

        /// <summary>
        /// Подпись или лицензия протухли, продлить их и попросить обработать документы
        /// </summary>
        [Description("Продлить подпись и создать ТС стеку")]
        ProlongEdmLicense = 5,

        /// <summary>
        /// Подпись или лицензия скоро протухнут
        /// </summary>
        [Description("Подпись скоро просрочится")]
        EdmLicenseAboutToExpire = 6,

        /// <summary>
        /// Есть непрочитанные входящие, если через полчаса их число не уменьшится - создать ТС на нас
        /// </summary>
        [Description("Есть непрочитанные входящие, проверить, не уменьшится-ли их число")]
        RequestToFixConsole = 7,

        /// <summary>
        /// ЭП пользователя не готова
        /// </summary>
        [Description("Необходимо настроить ЭП пользователя")]
        RequestToCreateSignature = 8,

        /// <summary>
        /// Задвоенные контрагенты в EdmInvite
        /// </summary>
        [Description("Обнаружены задвоенные контрагенты в EdmInvite")]
        DuplicatesByKontragentDetected = 9,

        /// <summary>
        /// Задвоенные гуиды в EdmInvite
        /// </summary>
        [Description("Обнаружены задвоенные GUID'ы в EdmInvite")]
        DuplicatesByAbnGuidDetected = 10,

        /// <summary>
        /// Доступна настроенная связь по другому оператору
        /// </summary>
        [Description("Доступна настроенная связь по другому оператору")]
        CanChangeInviteToAnotherProvider = 11
    }
}
