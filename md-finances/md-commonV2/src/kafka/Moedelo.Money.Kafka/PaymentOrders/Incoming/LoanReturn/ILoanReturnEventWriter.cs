using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanReturn.Events;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanReturn
{
    public interface ILoanReturnEventWriter : IDI
    {
        Task WriteAsync(int firmId, int userId, LoanReturnUpdatedMessage message);
    }
}