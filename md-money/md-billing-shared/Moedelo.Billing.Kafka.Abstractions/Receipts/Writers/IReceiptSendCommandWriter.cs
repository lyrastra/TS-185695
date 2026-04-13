using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions.Receipts.Commands;

namespace Moedelo.Billing.Kafka.Abstractions.Receipts.Writers;

public interface IReceiptSendCommandWriter
{
    Task WriteAsync(ReceiptSendCommand command);
}