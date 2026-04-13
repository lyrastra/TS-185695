using System;
using Moedelo.Billing.Kafka.Abstractions.Bills.Enums;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Billing.Kafka.Abstractions.Bills.Commands;

public class SendBillRemindersCommand : IEntityCommandData
{
    public Guid RequestGuid { get; set; }
    public ReminderMethodEnum ReminderMethod { get; set; }
}