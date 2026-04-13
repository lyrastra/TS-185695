namespace Moedelo.Common.Enums.Enums.Integration
{
    public enum IntegrationCallType
    {
        /// <summary> Включение интеграции </summary>
        IntegrationTurn = 0,

        /// <summary> Отправка платёжки </summary>
        SendPaymentOrder = 1,

        /// <summary> Запрос выписки </summary>
        RequestMovementList = 2,

        /// <summary> Подтверждение включение интеграции смс кодом </summary>
        IntegrationTurnOtp = 3,

        /// <summary> Запрос сверки с банком из партнерки </summary>
        ReviseForBackoffice = 5,

        /// <summary> Запрос сверки с банком для пользователя </summary>
        ReviseForUser = 6,

        /// <summary> Включение интеграции </summary>
        IntegrationTurnOff = 7,

        /// <summary> SSO авторизация </summary>
        SsoAuthorization = 8,

        /// <summary> Создание черновика платежного реестра </summary>
        SalaryPaymentRegistryCreate = 9,

        /// <summary> Запрос выписки без уведомления пользователя </summary>
        SilentRequestMovementList = 10,

        /// <summary> Отправка сквозного бюджетного платежа </summary>
        SendBudgetInvoice = 11,

        /// <summary> Запросы lag выписок </summary>
        LagRequestMovementList = 12,
        
        /// <summary> Отправка сквозного (прямого) платежа по свободным реквизитам </summary>
        SendAnyInvoice = 13,
        
        /// <summary>Запрос выписки после сквозного (прямого) платежа</summary>
        InvoicedRequestMovement = 14,

        /// <summary> Запрос выписки за субботу (за 3 месяц) </summary>
        MissedRequestMovement = 15,

        /// <summary> Запрос выписки после оплаты </summary>
        PostPaymentRequestMovement = 16,
        
        /// <summary> Запрос выписки за текущий день без участия пользователя </summary>
        RequestAutoTodayMovement = 17
    }
}