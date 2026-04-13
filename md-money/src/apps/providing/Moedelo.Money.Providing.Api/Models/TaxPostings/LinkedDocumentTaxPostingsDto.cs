using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.Json.Convertors;
using Moedelo.Money.Providing.Business.Abstractions.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Api.Models.TaxPostings
{
    public class LinkedDocumentTaxPostingsDto<T> where T : ITaxPostingDto
    {
        [DateValue]
        [JsonConverter(typeof(IsoDateConverter))]
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public LinkedDocumentType Type { get; set; }
        public IReadOnlyCollection<T> Postings { get; set; }
    }
}
