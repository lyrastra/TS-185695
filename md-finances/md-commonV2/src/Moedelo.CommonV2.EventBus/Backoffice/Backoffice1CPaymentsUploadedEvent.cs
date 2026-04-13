namespace Moedelo.CommonV2.EventBus.Backoffice
{
    /// <summary>
    /// Событие об успешной загрузке выписки 1С.
    /// Содержит идентификатор выписки и ключ JSON-файла с результатами парсинга в облачном хранилище.
    /// </summary>
    public class Backoffice1CPaymentsUploadedEvent
    {
        /// <summary> Идентификатор выписки (PaymentImportHistory.Id) </summary>
        public int HistoryId { get; set; }

        /// <summary> Уникальный ключ файла в облачном хранилище (результаты парсинга в JSON) </summary>
        public string ParsedDataStorageKey { get; set; }
    }
}
