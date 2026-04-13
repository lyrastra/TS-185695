namespace Moedelo.BankIntegrations.Enums
{
    public enum RequestStatus : byte
    {
        /// <summary> Запрос отправлен в банковскую очередь </summary>
        AwaitInQueue = 0,

        /// <summary> Ответ успешно получен и обработан </summary>
        CompletedSuccess = 1,

        Error = 3,

        /// <summary> Запрос заморожен на время выполнения Машинами "INT-PROD01" или "INT-STAGE" </summary>
        FreezingInQueue1 = 4,

        /// <summary> Запрос создан, но не обработан</summary>
        Created = 5,

        /// <summary> Запрос заморожен на время выполнения Машинами "INT-PROD02" </summary>
        FreezingInQueue2 = 6,

        /// <summary> Запрос отправлен в банк, ожидание когда банк пришлёт выписку к нам </summary>
        AwaitForPush = 7,

        /// <summary> Ответ успешно получен и обработан, но не отправлен в импорт т.к. дубликат. Применяется для неполных выписок IntegrationCallType = 12  </summary>
        DuplicateCompletedSuccess = 12,
    }
}
