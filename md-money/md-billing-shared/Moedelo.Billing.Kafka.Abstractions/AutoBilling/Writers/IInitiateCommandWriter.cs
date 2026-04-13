using System.Threading.Tasks;
using Moedelo.Billing.Kafka.Abstractions.AutoBilling.Commands;

namespace Moedelo.Billing.Kafka.Abstractions.AutoBilling.Writers;

public interface IInitiateCommandWriter
{
    Task SendStartAutoInitiateCommandAsync(StartAutoInitiateCommand commandData);
}