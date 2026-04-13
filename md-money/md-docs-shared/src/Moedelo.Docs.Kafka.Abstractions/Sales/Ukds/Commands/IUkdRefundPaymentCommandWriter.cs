using System.Threading.Tasks;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.Ukds.Commands
{
    public interface IUkdRefundPaymentCommandWriter
    {
        Task WriteAsync(long documentBaseId);
    }
}