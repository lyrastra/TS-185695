using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Models
{
    public class PaymentOrderDuplicateDataResponse
    {
        public OperationType OperationType { get; set; }

        public DateTime Date { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }
    }
}
