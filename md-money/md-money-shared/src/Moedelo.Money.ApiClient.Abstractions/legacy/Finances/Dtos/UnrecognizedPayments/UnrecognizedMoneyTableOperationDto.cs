using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos.UnrecognizedPayments
{
    public class UnrecognizedMoneyTableOperationDto
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public MoneyDirection Direction { get; set; }

        public OperationType OperationType { get; set; }
    }
}
