using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.Commands.ActualizeFromImport
{
    public interface IOutgoingPaymentOrdersCommandWriter : IDI
    {
        Task WriteActualizeAsync(int firmId, int userId, ActualizeFromImport commandData);
    }
}