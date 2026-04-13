using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(InventoryCardValidator))]
    internal sealed class InventoryCardValidator
    {
        private readonly InventoryCardReader reader;

        public InventoryCardValidator(InventoryCardReader inventoryCardReader)
        {
            this.reader = inventoryCardReader;
        }

        public async Task ValidateAsync(long? inventoryCardBaseId)
        {
            if (!inventoryCardBaseId.HasValue)
            {
                throw new BusinessValidationException("InventoryCard.DocumentBaseId", "Необходимо указать инвентарную карту");
            }
            
            var inventoryCard = await reader.GetByBaseIdAsync(inventoryCardBaseId.Value);
            if (inventoryCard == null)
            {
                throw new BusinessValidationException("InventoryCard.DocumentBaseId", $"Не найдена инвентарная карта с ид {inventoryCardBaseId}");
            }
        }
    }
}
