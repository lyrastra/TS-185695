using System;
using System.Collections.Generic;
using System.Text;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos.UnrecognizedPayments
{
    public class MoneyTableRequestDto
    {
        public int Count { get; set; } = 20;
        public int Offset { get; set; } = 0;
        public MoneySourceType SourceType { get; set; } = MoneySourceType.All;
        public long? SourceId { get; set; }
    }
}
