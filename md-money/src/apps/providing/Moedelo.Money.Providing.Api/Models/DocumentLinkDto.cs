using Moedelo.Infrastructure.AspNetCore.Validation;

namespace Moedelo.Money.Providing.Api.Models
{
    public class DocumentLinkDto
    {
        /// <summary>
        /// Базовый ИД документа
        /// </summary>
        [IdLongValue]
        [RequiredValue]
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Сумма учитываемая в оплате
        /// </summary>
        [SumValue]
        [RequiredValue]
        public decimal Sum { get; set; }
    }
}
