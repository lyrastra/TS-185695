using System;
using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesUkd.Model
{
    public class Ukd
    {
        public long DocumentBaseId { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
        public int KontragentId { get; set; }
        public long SourceDocumentBaseId { get; set; }
        public long? AdditionalDocumentBaseId { get; set; }
        public bool ProvideInAccounting { get; set; }
        public UkdSourceType UkdSourceType { get; set; }
        public List<UkdItem> Items { get; set; }

    }
}