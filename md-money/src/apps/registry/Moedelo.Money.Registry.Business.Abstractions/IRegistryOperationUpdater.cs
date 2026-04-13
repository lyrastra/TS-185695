using Moedelo.Money.Registry.Domain.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.Registry.Business.Abstractions
{
    public interface IRegistryOperationUpdater
    {
        public Task CreateAsync(CreateMoneyOperationCommand command);
        public Task UpdateAsync(UpdateMoneyOperationCommand command);
        public Task DeleteAsync(DeleteMoneyOperationCommand command);
    }
}
