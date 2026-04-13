using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions.Bills.Commands;

namespace Moedelo.Billing.Kafka.Abstractions.Bills.Writers;

public interface ISendBillRemindersCommandWriter
{
    Task WriteAsync(SendBillRemindersCommand message);
}