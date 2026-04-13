using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions.Bills.Commands;

namespace Moedelo.Billing.Kafka.Abstractions.Bills.Writers
{
    public interface IInvoiceBillCommandWriter
    {
        Task WriteAsync(InvoiceBillKafkaCommandData message);
    }
}