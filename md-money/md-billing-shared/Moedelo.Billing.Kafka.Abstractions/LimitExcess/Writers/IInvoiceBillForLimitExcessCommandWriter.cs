using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Billing.Kafka.Abstractions.LimitExcess.Writers
{
    public interface IInvoiceBillForLimitExcessCommandWriter
    {
        Task WriteAsync<T>(T message, string key) where T : IEntityCommandData;
    }
}