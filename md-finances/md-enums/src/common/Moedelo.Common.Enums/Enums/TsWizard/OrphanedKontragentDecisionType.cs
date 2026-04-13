using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.TsWizard
{
    public enum OrphanedKontragentDecisionType
    {
        /// <summary>
        /// К моменту вызова принимателя решений контрагент перестал быть сиротой
        /// </summary>
        [Description("Все в порядке")]
        EverythingIsOK = 0,

        /// <summary>
        /// Контрагент есть у пользователя, но отсутствует в EdmInvites
        /// </summary>
        [Description("Добавить контрагента в адресную книгу")]
        AddToEdmInvites = 1,

        /// <summary>
        /// Контрагент вообще отсутствует у пользователя, надо добавить его в список
        /// </summary>
        [Description("Добавить контрагента")]
        AddToKontragents = 2,

        /// <summary>
        /// Не удалось принять решение
        /// </summary>
        [Description("Создать ТС на разработчиков")]
        UnableToMakeDecision = 3
    }
}
