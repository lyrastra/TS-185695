using System;
using System.Collections.Generic;

namespace Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos
{
    public class PostingDto
    {
        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int Debit { get; set; }

        public IList<SubcontoDto> DebitSubcontos { get; set; }

        public int Credit { get; set; }

        public IList<SubcontoDto> CreditSubconto { get; set; }

        public string Description { get; set; }
    }
}