namespace Moedelo.OfficeV2.Dto.Egr.UlModel.Models
{
    /// <summary>
    /// Сведения об исполнительном органе Фонда социального страхования Российской Федерации
    /// </summary>
    public class EgrUlFssInfoDto
    {
        /// <summary>
        /// Код по справочнику СТОФСС
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }
    }
}