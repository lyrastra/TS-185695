using Moedelo.AccountingStatements.Enums;
using System;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.PaymentForDocuments
{
    public class PaymentForDocumentCreateRequestDto
    {
        /// <summary>
        /// Временный идентификатор, для соотнесения с результатами ответа
        /// </summary>
        public Guid TemporaryId { get; set; }

        /// <summary>
        /// Описание документа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Направление платежа и первичного документа
        /// </summary>
        public PaymentForDocumentType Type { get; set; }

        /// <summary>
        /// Тип системы налогообложения для которой будет применена бухгалтерская справка. Необходимо выбирать только из действующих на дату создания документа систем. [1] = УСН, [2] = ОСНО, [3] = ЕНВД
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Бухгалтерская проводка
        /// </summary>
        public AccountingPostingDto AccountingPosting { get; set; }
    }
}
