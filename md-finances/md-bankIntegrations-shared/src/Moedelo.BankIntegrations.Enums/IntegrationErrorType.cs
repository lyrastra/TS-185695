namespace Moedelo.BankIntegrations.Enums
{
    /// <summary> Тип ошибки интегрированных банков </summary>
    public enum IntegrationErrorType
    {
        DefaultError = 0,

        Success = 1,

        /// <summary>
        /// Для состояний когда запрос в банк еще не ушел
        /// </summary>
        Processing = 2,

        #region Sberbank (10270000 - 10279999)
        /// <summary> Сбербанк. Общая ошибка о том, что не подписана оферта </summary>
        /// Зарезервировано с 0010 по 0019
        SberbankOfferNotSigning = 10270010,

        /// <summary> Сбербанк. Общая ошибка при включении интеграции </summary>
        /// Пока не используется
        /// Зарезервировано с 0020 по 0029
        SberbankIntegrationTurnOnCommonError = 10270020,

        /// <summary> Сбербанк. Общая ошибка в платежном поручении </summary>
        /// Зарезервировано с 0030 по 0129
        SberbankPaymentOrderCommonError = 10270030,

        /// <summary> Сбербанк. Ошибка при запросе выписки </summary>
        /// Зарезервировано с 0130 по 0229
        SberbankMovementsCommonError = 10270130,

        /// <summary> Сбербанк. Не разрешен один или несколько (все) счет(-а, -ов) </summary>
        SberbankSettlementNotAllowedError = 10270131,

        /// <summary> Операции по счету недоступны. Необходимо добавить нужный счет, переподписать оферту в банке и переподключить интеграцию. На фронте есть обработка для этого типа.</summary>
        SberbankSettlementUnavailableError = 10270132,

        /// <summary>Произошла ошибка с обновлением токена, необходимо авторизоваться в банке повторно.</summary>
        SberbankSettlementRefreshTokenError = 10270133,

        /// <summary>Операции по счету недоступны. Необходимо добавить нужный счет, переподписать оферту в банке и переподключить интеграцию</summary>
        SberbankSettlementActionAccessError = 10270134,

        /// <summary>Выписка по счету недоступна в банке. Повторите запрос позднее. Ошибка 404 от Сбера</summary>
        SberbankSettlementDataNotFoundError = 10270135,

        /// <summary>Выписка по счету была получена не за все дни, счёт не является действующим на запрошенную(ые) дату(ы): . Проверьте, является ли счёт действующим в банке на запрошенную(ые) дату(ы).</summary>
        SberbankSettlementWorkflowFault = 10270136,

        /// <summary>Выписка по счету была получена не за все дни, счёт не является действующим на запрошенную(ые) дату(ы)</summary>
        SberbankSettlementStatementIsNotReadyError = 10270137,

        /// <summary>Выписка по счету за указанную дату недоступна. Произошла неизвестная ошибка, обратитесь в техническую поддержку, support@moedelo.org или по бесплатному телефону 8 800 200 77 27.</summary>
        SberbankSettlementBadRequestError = 10270138,

        /// <summary>На стороне сбербанка произошла техническая ошибка</summary>
        SberbankServiceUnavailableError = 10270139,

        /// <summary>Счета нет в ClientInfo в IntegrationData</summary>
        SberbankSettlementNotExistsInClientInfo = 10270140,
        
        /// <summary>Счёт существует, но не был активен (не открыт или уже закрыт) на запрашиваемую дату.</summary>
        SberbankSettlementNotActiveOnRequestedDate = 10270141,
        #endregion
        
        #region Tinkoff (10590000 - 10599999)
        /// <summary>Запрещен доступ к компании для пользователя в ТБанк</summary>
        TinkoffForbiddenAccessCompany = 10590001,
        #endregion
        
        #region Point (10640000 - 10649999)
        /// <summary>Точка. Срок действия согласия истек </summary>
        PointConsentReSigning = 10640001,
        #endregion
        
        #region Alfa (10710000 - 10719999)
        /// <summary>Альфа. Переподписать согласие, которые выдано на год за N дней перед окончанием </summary>
        AlfaConsentReSigning = 10710001,
        /// <summary>Альфа. Срок действия согласия истек </summary>
        AlfaConsentExpired = 10710002
        #endregion
    }
}
