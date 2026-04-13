using System;
using Moedelo.Billing.Shared.Enums.AutoBilling;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Billing.Kafka.Abstractions.AutoBilling.Commands;

public class StartAutoInitiateCommand : IEntityCommandData
{
    public DateTime ExpirationDate { get; set; }
    public ProductTypeEnum ProductType { get; set; }
}