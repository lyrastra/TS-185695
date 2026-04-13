using System;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.PaymentForDocuments
{
    public class PaymentForDocumentCreateResponseDto
    {
        /// <summary>
        /// Временный идентификатор, для соотнесения с данными запроса
        /// </summary>
        public Guid TemporaryId { get; set; }

        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа (автоматическая нумерация при создании)
        /// </summary>
        public string Number { get; set; }
    }
}
