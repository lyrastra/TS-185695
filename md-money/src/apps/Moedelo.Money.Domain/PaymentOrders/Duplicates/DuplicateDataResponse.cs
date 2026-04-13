using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Money.Domain.PaymentOrders.Duplicates
{
    public class DuplicateDataResponse
    {
        public OperationType OperationType { get; set; }

        public DateTime Date { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }
    }
}
