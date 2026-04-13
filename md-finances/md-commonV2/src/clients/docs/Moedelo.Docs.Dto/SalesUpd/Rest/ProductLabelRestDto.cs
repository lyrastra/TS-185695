using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.Docs.Dto.SalesUpd.Rest
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