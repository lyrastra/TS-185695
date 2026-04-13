namespace Moedelo.Docs.Dto.ProductsTrace
{
    public class ProductTraceDto
    {
        /// <summary>
        /// РНПТ/номер ГТД
        /// </summary>
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Код единицы измерения
        /// </summary>
        public string UnitCode { get; set; }

        /// <summary>
        /// Кол-во прослеживаемого товара
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Идентификатор страны происхождения товара
        /// </summary>
        public string CountryIso { get; set; }

        /// <summary>
        /// Сумма без НДС прослеживаемого товара
        /// </summary>
        public decimal SumWithoutNds { get; set; }
    }
}