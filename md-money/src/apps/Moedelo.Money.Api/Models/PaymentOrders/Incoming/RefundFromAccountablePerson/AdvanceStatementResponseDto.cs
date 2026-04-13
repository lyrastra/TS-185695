using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Newtonsoft.Json;
using System;

namespace Moedelo.Money.Api.Models.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    public class AdvanceStatementResponseDto
    {
        /// <summary>
        /// Идентификатор авансового отчета
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата авансового отчета
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер авансового отчета
        /// </summary>
        public string Number { get; set; }
    }
}
