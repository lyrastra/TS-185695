using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto
{
    public class ProductLabelRestDto
    {
        /// <summary>
        /// Значение кода маркировки
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Тип кода маркировки
        /// </summary>
        public ProductLabelType Type { get; set; }
    }
}