using System;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Domain.LinkedDocuments;
using Newtonsoft.Json;

namespace Moedelo.Money.Api.Models.LinkedDocuments
{
    public class DocumentLinkDto
    {
        /// <summary>
        /// Идентификатор первичного документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Тип первичного документа
        /// Накладная = 1
        /// Акт = 6
        /// Входящий универсальный передаточный документ = 33
        /// Исходящий универсальный передаточный документ = 36
        /// Исходящий инвойс = 55
        /// </summary>
        [EnumValue(EnumType = typeof(DocumentType))]
        public DocumentType Type { get; set; }

        /// <summary>
        /// Дата первичного документа
        /// </summary>
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер первичного документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сумма первичного документа
        /// </summary>
        [SumValue]
        public decimal DocumentSum { get; set; }

        /// <summary>
        /// Учитываемая сумма
        /// </summary>
        [SumValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма, оплаченная в другом платежном документе
        /// </summary>
        [SumValue]
        public decimal PaidSum { get; set; }
    }
}
