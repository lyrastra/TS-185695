using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto.Providing.AccPostings
{
    public class LinkedDocumentAccPostingsDto
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public LinkedDocumentType Type { get; set; }

        public IReadOnlyCollection<AccPostingDto> Postings { get; set; }
    }
}
