using Moedelo.Money.Registry.Domain.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.Registry.Business.Abstractions
{
    public interface IRegistryOperationEventWriter
    {
        Task WriteCreatedEventAsync(CreateMoneyOperationCommand command);
        Task WriteUpdatedEventAsync(UpdateMoneyOperationCommand command);
        Task WriteDeletedEventAsync(DeleteMoneyOperationCommand command);
    }
}