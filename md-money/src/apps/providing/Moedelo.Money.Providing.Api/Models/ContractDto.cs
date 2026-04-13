using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Providing.Api.Models
{
    public class ContractDto
    {
        /// <summary>
        /// Идентификатор договора
        /// </summary>
        [IdLongValue]
        public long? DocumentBaseId { get; set; }
    }
}
