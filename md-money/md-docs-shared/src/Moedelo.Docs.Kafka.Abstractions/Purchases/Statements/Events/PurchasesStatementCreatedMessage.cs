using System;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.Statements.Events
{
    public class PurchasesStatementCreatedMessage
    {
        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

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
        /// Провести в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Откуда создан документ
        /// </summary>
        public CreationPlace CreationPlace { get; set; }
    }
}
