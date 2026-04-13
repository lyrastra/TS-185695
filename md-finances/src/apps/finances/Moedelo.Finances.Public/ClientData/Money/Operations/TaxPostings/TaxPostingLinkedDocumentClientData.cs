using System;
using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.InfrastructureV2.Json.Converters;
using Newtonsoft.Json;

namespace Moedelo.Finances.Public.ClientData.Money.Operations.TaxPostings
{
    public class TaxPostingLinkedDocumentClientData
    {
        public string DocumentName { get; set; }

        public string DocumentNumber { get; set; }

        [JsonConverter(typeof(MdDateConverter))]
        public DateTime DocumentDate { get; set; }

        public AccountingDocumentType Type { get; set; }

        public List<TaxPostingDescriptionClientData> Postings { get; set; }

        public ProvidePostingType PostingsAndTaxMode { get; set; }
    }
}