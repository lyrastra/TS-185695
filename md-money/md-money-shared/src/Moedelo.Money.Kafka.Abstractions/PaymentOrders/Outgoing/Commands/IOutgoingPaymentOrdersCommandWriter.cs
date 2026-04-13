using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ChangeIsPaidFromIntegration;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands
{
    public interface IOutgoingPaymentOrdersCommandWriter
    {
        Task WriteActualizeAsync(ActualizeFromImport.ActualizeFromImport commandData);
        
        Task WriteChangeIsPaidAsync(ChangeIsPaidFromIntegrationItem commandData);
    }
}