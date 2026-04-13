using System.Threading.Tasks;
using Moedelo.Billing.Kafka.NetFramework.Abstractions.Receipts.Commands;

namespace Moedelo.Billing.Kafka.NetFramework.Abstractions.Receipts.Writers;

public interface IReceiptSendCommandWriter
{
    Task WriteAsync(ReceiptSendCommand command);
}