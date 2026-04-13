namespace Moedelo.BankIntegrations.Enums.Acceptance
{
    public enum AcceptanceBankStatus
    {
        /// <summary>
        /// Custom status: статус инициализации
        /// </summary>
        UNKNOWN = -1,
        /// <summary>
        /// Документ создан и прошел первичный контроль на КЧ.
        /// </summary>
        CREATED,
        /// <summary>
        /// Недокументированный статус
        /// </summary>
        DELIVERED,
        /// <summary>
        /// При прохождении контролей на КЧ, в процессе сохранения, обнаружены ошибки.
        /// </summary>
        CHECKERROR,
        /// <summary>
        /// Документ удален Пользователем
        /// </summary>
        DELETED,
        /// <summary>
        /// Документ подписан не полным набором подписей.
        /// </summary>
        PARTSIGNED,
        /// <summary>
        /// Документ подписан полным набором подписей.
        /// </summary>
        SIGNED,
        /// <summary>
        /// Недокументированный статус
        /// </summary>
        ACCEPTED,
        /// <summary>
        /// Документ принят АБС Банка.
        /// </summary>
        ACCEPTED_BY_ABS,
        /// <summary>
        /// Документ отказан АБС Банка.
        /// </summary>
        REFUSEDBYABS,
        /// <summary>
        /// Документ отказан сотрудником Банка по результатам ручной обработки.
        /// </summary>
        REFUSEDBYBANK,
        /// <summary>
        /// В процессе контроля в АБС Банка обнаружены ошибки в реквизитах документа.
        /// </summary>
        REQUISITEERROR,
        /// <summary>
        /// В процессе контроля в АБС Банка обнаружена ошибка подписи документа.
        /// </summary>
        INVALIDEDS,
        /// <summary>
        /// 1. Документ исполнен Банком. В ЕКС создано Распоряжение на ЗДА. (Исполнен)
        /// 2. Документ исполнен Банком. Есть связанное заявление на отмену действия ЗДА. (для заявления на заранее данный акцепт)
        /// </summary>
        IMPLEMENTED,
        /// <summary>
        /// Истек срок действия заранее данного акцепта.
        /// </summary>
        ACCEPTEXPIRE,
        /// <summary>
        /// Дата отмены действия ЗДА меньше (раньше) или равна текущей дате. Заранее данный акцепт (Распоряжение) отменен.
        /// </summary>
        RECALL,
        /// <summary>
        /// Custom status: Ошибка при создании ЗДА Лайт в банке
        /// </summary>
        BANK_INTERNAL_SERVER_ERROR
    }
}