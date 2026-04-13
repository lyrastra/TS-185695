using Moedelo.Billing.Kafka.Common.Receipts.Commands;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Billing.Kafka.NetFramework.Abstractions.Receipts.Commands;

/// <summary>
/// Команда на отправку чека об оплате
/// </summary>
public sealed class ReceiptSendCommand : ReceiptSendCommandFields, IEntityCommandData
{ 
}