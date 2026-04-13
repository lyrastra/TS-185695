namespace Moedelo.Common.Enums.Enums.Integration
{
    public enum IntegrationRequestPartStatus
    {
        /// <summary> Запрос сохранён в очередь </summary>
        AwaitInQueue = 0,

        /// <summary> Ответ успешно получен и обработан </summary>
        DoneSuccess = 1,

        /// <summary> Ответ на запрос подготовлен на стороне банка (только для банков поддерживающих такой статус) </summary>
        ProcessedInBank = 2,

        /// <summary> Запрос завершился ошибкой </summary>
        DoneError = 3,

        /// <summary> Запрос выполняется на первой машине </summary>
        RunOnMachine1 = 4,

        /// <summary> Запрос создан, но не обработан</summary>
        Created = 5,

        /// <summary> Запрос выполняется на второй машине </summary>
        RunOnMachine2 = 6,

        /// <summary> Запрос отправлен в банк, ожидание когда банк пришлёт выписку к нам </summary>
        AwaitForPush = 7
    }
}