namespace Moedelo.BankIntegrations.Enums
{
    public enum REventType
    {
        /// <summary> Запрос выписки по PUSH схеме </summary>
        MovementRequestPush = 0,

        /// <summary> Запрос выписки по синхронной схеме </summary>
        MovementRequestSync = 1,

        /// <summary> Запрос выписки по схеме с очередями </summary>
        MovementRequestQueue = 2,

        /// <summary> Импорт запушенного 1C файла </summary>
        Push1CFile = 3,

        /// <summary> Опрос статуса выписки для схемы с очередями </summary>
        GetMovementStatus = 4,

        /// <summary> Забор готовой выписки для схемы с очередями </summary>
        GetReadyMovement = 5,

        /// <summary> Опрос статуса созданного сквозного п/п </summary>
        GetInvoicePaymentOrderStatus = 6,

        /// <summary>Отложенное создание счетов при включении интеграции или регистрации</summary>
        CreateAccounts = 7,

        /// <summary>Запрос выписки после успешного статуса прямого платежа</summary>
        MovementRequestInvoiced = 8,
    }
}
