using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.PurseOperations.Domain.Models
{
    public class PurseOperation
    {
        public OperationType OperationType { get; set; }

        public DateTime Date { get; set; }
    }
}
