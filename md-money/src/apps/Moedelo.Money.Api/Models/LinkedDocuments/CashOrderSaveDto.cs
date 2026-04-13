using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models.LinkedDocuments
{
    public class CashOrderSaveDto
    {
        /// <summary>
        /// Идентификатор кассового ордера
        /// </summary>
        [IdLongValue]
        public long? DocumentBaseId { get; set; }
    }
}
