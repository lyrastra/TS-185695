using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Api.Models.LinkedDocuments
{
    /// <summary>
    /// Связь с первичным документом
    /// </summary>
    public class DocumentLinkSaveDto
    {
        /// <summary>
        /// Идентификатор первичного документа
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