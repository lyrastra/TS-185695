using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Statements.Events
{
    public sealed class SalesStatementUpdatedMessage
    {
        /// <summary>
        /// BaseId документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int KontragentId { get; set; }

        /// <summary>
        /// Идентификтор договора (не DocumentBaseId, сделать его оказалоь дорого)
        /// Сервис, как правило, оперирует DocumentBaseId (во внешних api - обязательное условие).
        /// Стоит понимать разницу и учитывать риски при использования данного поля, консультироваться с владельцами домена.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Статус подписи. (Не подписан, Скан, Подписан)
        /// </summary>
        public SignStatus SignStatus { get; set; }

        /// <summary>
        /// Провести в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }
    }
}
