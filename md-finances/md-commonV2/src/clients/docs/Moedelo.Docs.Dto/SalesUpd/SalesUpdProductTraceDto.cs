namespace Moedelo.Docs.Dto.SalesUpd
{
    public class SalesUpdProductTraceDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор фирмы
        /// </summary>
        public int FirmId { get; set; }
        
        /// <summary>
        /// Базовый идентифкатор документа 
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Идентификатор позиции документа
        /// </summary>
        public int ItemId { get; set; }

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
