using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models.LinkedDocuments
{
    /// <summary>
    /// Связь со счётом
    /// </summary>
    public class BillLinkSaveDto
    {
        /// <summary>
        /// Идентификатор счёта
        /// </summary>
        [IdLongValue]
        [RequiredValue]
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Учитываемая сумма
        /// </summary>
        [SumValue]
        [RequiredValue]
        public decimal Sum { get; set; }
    }
}