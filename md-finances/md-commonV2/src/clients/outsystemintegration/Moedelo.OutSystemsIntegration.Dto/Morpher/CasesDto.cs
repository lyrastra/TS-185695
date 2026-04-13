namespace Moedelo.OutSystemsIntegrationV2.Dto.Morpher
{
    public class CasesDto
    {
        /// <summary>
        /// Именительный падеж
        /// </summary>
        public string Nominative { get; set; }
        /// <summary>
        /// Родительный падеж
        /// </summary>
        public string Genitive { get; set; }
        /// <summary>
        /// Дательный падеж
        /// </summary>
        public string Dative { get; set; }
        /// <summary>
        /// Винительный падеж
        /// </summary>
        public string Accusative { get; set; }
        /// <summary>
        /// Творительный падеж
        /// </summary>
        public string Ablative { get; set; }
        /// <summary>
        /// Предложный падеж
        /// </summary>
        public string Prepositional { get; set; }
    }
}