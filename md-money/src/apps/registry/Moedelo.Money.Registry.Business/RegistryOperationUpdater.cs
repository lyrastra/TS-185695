using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Registry.Business.Abstractions;
using Moedelo.Money.Registry.Domain.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.Registry.Business
{
    [InjectAsSingleton(typeof(IRegistryOperationUpdater))]
    class RegistryOperationUpdater : IRegistryOperationUpdater
    {
        private readonly IRegistryOperationEventWriter eventWriter;

        public RegistryOperationUpdater(
            IRegistryOperationEventWriter eventWriter)
        {
            this.eventWriter = eventWriter;
        }

        public Task CreateAsync(CreateMoneyOperationCommand command)
        {
            // save to product db maybe?
            return eventWriter.WriteCreatedEventAsync(command);
        }

        public Task UpdateAsync(UpdateMoneyOperationCommand command)
        {
            // save to product db maybe?
            return eventWriter.WriteUpdatedEventAsync(command);
        }

        public Task DeleteAsync(DeleteMoneyOperationCommand command)
        {
            // save to product db maybe?
            return eventWriter.WriteDeletedEventAsync(command);
        }
    }
}
