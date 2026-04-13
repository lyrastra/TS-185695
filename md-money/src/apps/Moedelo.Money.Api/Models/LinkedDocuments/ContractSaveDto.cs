using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models.LinkedDocuments
{
    public class ContractSaveDto
    {
        /// <summary>
        /// Идентификатор договора
        /// </summary>
        [IdLongValue]
        public long? DocumentBaseId { get; set; }
    }
}
