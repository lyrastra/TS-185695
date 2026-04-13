using Moedelo.Money.Kafka.PaymentOrders.Outgoing.Commands.ActualizeFromImport;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.BankFee
{
    public interface IOutgoingCommandWriter
    {
        Task WriteActualizeAsync(int firmId, int userId, ActualizeFromImport commandData);
    }
}