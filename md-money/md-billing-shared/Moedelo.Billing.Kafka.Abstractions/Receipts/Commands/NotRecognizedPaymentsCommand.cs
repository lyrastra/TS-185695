using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Billing.Kafka.Abstractions.Receipts.Commands;

public class NotRecognizedPaymentsCommand : IEntityCommandData
{
    public DateTime? ImportPaymentsDate { get; set; }
}