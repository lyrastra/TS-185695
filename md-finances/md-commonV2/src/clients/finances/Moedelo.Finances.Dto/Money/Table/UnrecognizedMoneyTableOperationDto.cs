using System;
using Moedelo.Common.Enums.Enums.FinancialTransactions;
using Moedelo.Common.Enums.Enums.Money;

namespace Moedelo.Finances.Dto.Money.Table
{
    public class UnrecognizedMoneyTableOperationDto
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public MoneyDirection Direction { get; set; }

        public OperationType OperationType { get; set; }
    }
}
