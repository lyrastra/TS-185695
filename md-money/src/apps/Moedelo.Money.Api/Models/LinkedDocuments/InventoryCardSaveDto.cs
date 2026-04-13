using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models.LinkedDocuments
{
    public class InventoryCardSaveDto
    {
        /// <summary>
        /// Идентификатор инвентарной карты
        /// </summary>
        [IdLongValue]
        public long? DocumentBaseId { get; set; }
    }
}
