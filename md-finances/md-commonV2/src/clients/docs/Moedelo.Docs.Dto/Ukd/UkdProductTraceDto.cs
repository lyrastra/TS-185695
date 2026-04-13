namespace Moedelo.Docs.Dto.Ukd
{
    public class UkdProductTraceDto
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
        /// Кол-во прослеживаемого товара до изменений
        /// </summary>
        public decimal AmountBefore { get; set; }

        /// <summary>
        /// Кол-во прослеживаемого товара после изменений
        /// </summary>
        public decimal AmountAfter { get; set; }

        /// <summary>
        /// Идентификатор страны происхождения товара
        /// </summary>
        public string CountryIso { get; set; }

        /// <summary>
        /// Сумма без НДС прослеживаемого товара до изменений
        /// </summary>
        public decimal SumWithoutNdsBefore { get; set; }

        /// <summary>
        /// Сумма без НДС прослеживаемого товара после изменений
        /// </summary>
        public decimal SumWithoutNdsAfter { get; set; }
    }
}

